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
							 .UseCorsDependencies();

		builderDefination.App.MigrateDatabase();
	}

	public void DefineServices(WebApplicationBuilder builder)
	{
		builder.LogDependencies();
		builder.Services.RegisterResponseHandlerMiddlewares() 
						.RegisterDatabaseDependencies(builder.Configuration)
						.AddCorsDependencies()
						.InfrastructureDependencies()
						.ApplicationDependencies()
						.SharedApiClientHandlerDependencies();
	}
}