using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.ApplicationInsights;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using SampleProject.Domain;
using SampleProject.Domain.Entities;
using SampleProject.Shared.Helpers;
using SampleProject.Shared.Mvc.Filters;
using SampleProject.Application.Common.Constants;
using SampleProject.Application.Features.Account;
using SampleProject.Application.Services.DataInitialize;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerUI;
using FluentValidation;
using SampleProject.Application.Services.Authentication;

namespace SampleProject.Api.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void BuildConfiguration(this WebApplicationBuilder builder)
        {
            builder.Configuration
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{AppConstants.APP_ENVIRONMENT}.json", true, true)
                .AddEnvironmentVariables();
        }

        public static void BuildLogging(this WebApplicationBuilder builder)
        {
            if (!"local".Equals(AppConstants.APP_ENVIRONMENT))
            {
                var applicationInsightsConnectionString = builder.Configuration.GetConnectionString("ApplicationInsights");
                builder.Logging.AddApplicationInsights(
                    configureTelemetryConfiguration: (config) =>
                        config.ConnectionString = applicationInsightsConnectionString,
                    configureApplicationInsightsLoggerOptions: (options) =>
                    {
                        options.IncludeScopes = true;
                        options.TrackExceptionsAsExceptionTelemetry = false;
                    }
                );
                builder.Logging.AddFilter<ApplicationInsightsLoggerProvider>("sample-project-api", LogLevel.Trace);
            }
            else
            {
                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(builder.Configuration)
                    .CreateLogger();
            }
        }

        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            var services = builder.Services;
            var configuration = builder.Configuration;

            services.AddControllers(
                options =>
                {
                    options.Filters.Add(typeof(HttpGlobalExceptionFilter));
                    options.Filters.Add(typeof(ValidateModelFilter));
                })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                });
            services.AddSwaggerGenNewtonsoftSupport();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "SampleProject",
                    Description = "SampleProject API Docs",
                    TermsOfService = null,
                    Contact = new OpenApiContact { Name = "Phung Dinh", Email = "phungdkh@gmail.com", Url = null },
                });

                c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            },
                            Scheme = JwtBearerDefaults.AuthenticationScheme,
                            Name = JwtBearerDefaults.AuthenticationScheme,
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
                c.DescribeAllParametersInCamelCase();
                c.EnableAnnotations();
            });

            var dbConnectionString = configuration.GetConnectionString("AppDbConnection");
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.EnableSensitiveDataLogging();
                opt.UseNpgsql(dbConnectionString);
            });

            services.AddCors();
            services.AddHttpContextAccessor();
            services.AddMemoryCache();

            services.AddSampleProjectAuthorization(configuration);

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblies(ReflectionHelper.GetAssemblies());
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });
            services.AddValidatorsFromAssemblies(ReflectionHelper.GetAssemblies());

            // register all service
            services.AddScoped<IDataInitializeService, RoleDataInitializeService>();
            services.AddScoped<IDataInitializeService, UserDataInitializeService>();
            services.AddScoped<IAppAuthenticationService, AppAuthenticationService>();

            var container = new ContainerBuilder();
            container.Populate(services);

            ILifetimeScope autofacContainer = container.Build();
            var csl = new AutofacServiceLocator(autofacContainer);
            ServiceLocator.SetLocatorProvider(() => csl);
        }

        public static void AppUseAndRun(this WebApplicationBuilder builder)
        {
            var app = builder.Build();
            var serviceProvider = builder.Services.BuildServiceProvider();

            if ("local".Equals(AppConstants.APP_ENVIRONMENT))
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SampleProject API");
                    c.DocumentTitle = "SampleProject API";
                    c.DocExpansion(DocExpansion.None);
                });
            }

            app.UseHttpsRedirection();

            app.UseCors(x => x
               .SetIsOriginAllowed(_ => true)
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials());

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            var appDbContext = serviceProvider.GetRequiredService<AppDbContext>();
            appDbContext.Database.MigrateAsync().Wait();

            var dataInitializeServices = serviceProvider.GetServices<IDataInitializeService>().OrderBy(s => s.Order);
            foreach (var service in dataInitializeServices)
            {
                service.RunAsync().Wait();
            }

            app.Run();
        }
    }
}
