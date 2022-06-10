using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SampleProject.Application.Mvc.Filters;
using SampleProject.Domain;
using SampleProject.Domain.Entities;

namespace SampleProject.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSampleProjectAuthorization(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddIdentityCore<ApplicationUser>()
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetValue<string>("Jwt:AccessKey"))),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddAuthorizationBuilder()
                .AddPolicy(AppRolePolicy.USER_ACCESS, policy => policy.AddRequirements(new AppRolesAuthorizationRequirement(AppRolePolicy.USER_ACCESS_ROLES)))
                .AddPolicy(AppRolePolicy.ADMINISTRATOR_ACCESS, policy => policy.AddRequirements(new AppRolesAuthorizationRequirement(AppRolePolicy.ADMINISTRATOR_ACCESS_ROLES)));

            services.AddTransient<IAuthorizationHandler, AppRolesAuthorizationHandler>();
        }
    }
}
