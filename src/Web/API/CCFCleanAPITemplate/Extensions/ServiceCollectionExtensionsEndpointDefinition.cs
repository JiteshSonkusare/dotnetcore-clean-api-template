using NLog.Web;
using Application.Extensions;
using Infrastructure.Extensions;
using CCFCleanAPITemplate.EndpointDefinition.Models;
using CCFCleanAPITemplate.EndpointDefinition;
using CCFCleanAPITemplate.Middlewares;

namespace CCFCleanAPITemplate.Extensions;

public class ServiceCollectionExtensionsEndpointDefinition : IEndpointDefinition
{
    public void DefineEndpoints(AppBuilderDefinition builderDefination)
    {
        if (builderDefination.App.Environment.IsDevelopment())
            builderDefination.App.Services.MigrateDatabase();
        builderDefination.App.UseMiddleware<ApiResponseHandlerMiddleware>();
    }

    public void DefineServices(WebApplicationBuilder builder)
    {
        builder.Host.UseNLog();
        builder.Logging.ClearProviders().SetMinimumLevel(LogLevel.Trace);
        builder.Services.RegisterInfrastructureDependencies(builder.Configuration)
                        .RegisterApplicationDependencies();
    }
}