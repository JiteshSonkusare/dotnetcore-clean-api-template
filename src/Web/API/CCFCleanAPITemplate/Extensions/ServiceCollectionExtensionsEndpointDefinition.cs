using NLog.Web;
using Application.Extensions;
using Shared.ApiClientHanlder;
using Infrastructure.Extensions;
using CCFCleanAPITemplate.Middlewares;
using CCFCleanAPITemplate.EndpointDefinition;
using CCFCleanAPITemplate.EndpointDefinition.Models;

namespace CCFCleanAPITemplate.Extensions;

public class ServiceCollectionExtensionsEndpointDefinition : IEndpointDefinition
{
	public void DefineEndpoints(AppBuilderDefinition builderDefination)
	{
		builderDefination.App.UseMiddleware<ApiResponseHandlerMiddleware>()
							 .UseCors();

		builderDefination.App.MigrateDatabase();
	}

	public void DefineServices(WebApplicationBuilder builder)
	{
		builder.Host.UseNLog();
		builder.Logging.ClearProviders().SetMinimumLevel(LogLevel.Trace);

		builder.Services.RegisterDatabaseDependencies(builder.Configuration)
						.RegisterCorsDependencies()
						.RegisterInfrastructureDependencies()
						.RegisterApplicationDependencies()
						.RegisterSharedDependencies();
	}
}