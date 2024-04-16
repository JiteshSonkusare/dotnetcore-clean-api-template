using Application.Extensions;
using Shared.ApiClientHanlder;
using Infrastructure.Extensions;
using CCFClean.Minimal.Definition;
using CCFCleanAPITemplate.Middlewares;

namespace API.Extensions;

public class ServiceCollectionExtensionsEndpointDefinition : IEndpointDefinition
{
	public void DefineEndpoints(AppBuilderDefinition builderDefinition)
	{
		builderDefinition.App.UseMiddleware<ApiResponseHandlerMiddleware>()
							 .UseCorsDependencies();

		builderDefinition.App.MigrateDatabase();
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