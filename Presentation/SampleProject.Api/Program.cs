using SampleProject.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.BuildConfiguration();

builder.BuildLogging();

builder.ConfigureServices();

builder.AppUseAndRun();
