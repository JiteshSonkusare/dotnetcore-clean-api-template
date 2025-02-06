using Shared.Extension;
using Application.Extensions;
using Infrastructure.Extensions;
using CCFClean.Minimal.Definition;

namespace CCFCleanAPITemplate.Extensions;

public class ServiceCollectionExtensionsDefinition : IEndpointDefinition
{
	public void DefineEndpoints(AppBuilderDefinition builderDefinition)
	{
		builderDefinition.App.UseExceptionHandler()
							 .UseCorsDependencies();

		builderDefinition.App.MigrateDatabase();
	}

	public void DefineServices(WebApplicationBuilder builder)
	{
		builder.SetEnvironmentConfiguration();
		builder.LogDependencies();
		builder.Services.AddHttpContextAccessor();
		builder.Services.RegisterDatabaseDependencies(builder.Configuration)
						.AddCorsDependencies()
						.InfrastructureDependencies()
						.ApplicationDependencies()
						.SharedDependencies()
						.RegisterExceptionHandler();
	}
}