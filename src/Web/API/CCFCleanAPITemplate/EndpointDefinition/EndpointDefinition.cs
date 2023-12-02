using CCFCleanAPITemplate.Versioning;
using CCFCleanAPITemplate.EndpointDefinition.Models;
using CCFCleanAPITemplate.OpenApi.EndpointDefinition;

namespace CCFCleanAPITemplate.EndpointDefinition;

public static class EndpointDefinition
{
	public static void AddEndpointDefinitions(this WebApplicationBuilder builder, params Type[] scanMarkers)
	{
		var endpointDefinitions = new List<IEndpointDefinition>();

		foreach (var marker in scanMarkers)
		{
			endpointDefinitions.AddRange(
				marker.Assembly.ExportedTypes
					.Where(x => typeof(IEndpointDefinition).IsAssignableFrom(x)
					   && !x.IsInterface
					   && !x.IsAbstract
					   && !x.IsDefined(typeof(DeprecateEndpointDefinitionAttribute), false))
					.Select(Activator.CreateInstance)
					.Cast<IEndpointDefinition>());
		}

		foreach (var endpointDefinition in endpointDefinitions)
		{
			endpointDefinition.DefineServices(builder);
		}

		builder.Services.AddSingleton(endpointDefinitions as IReadOnlyCollection<IEndpointDefinition>);
	}

	public static void UseEndpointDefinitions(this WebApplication app, IEnumerable<DefineApiVersion>? defineApiVersions = null)
	{
		var apiVersionSet = app.ApiVersionSet(defineApiVersions ?? Enumerable.Empty<DefineApiVersion>());

		var definitions = app.Services.GetRequiredService<IReadOnlyCollection<IEndpointDefinition>>().OrderByDescending(def => def.GetType() != typeof(SwaggerEndpointDefinition));

		foreach (var endpointDefinition in definitions)
		{
			endpointDefinition.DefineEndpoints(new AppBuilderDefinition { App = app, ApiVersionSet = apiVersionSet });
		}
	}
}