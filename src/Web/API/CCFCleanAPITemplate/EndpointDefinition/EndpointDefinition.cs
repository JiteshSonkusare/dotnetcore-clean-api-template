using CCFCleanAPITemplate.Versioning;
using CCFCleanAPITemplate.EndpointDefinition.Models;

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
                    .Where(x => typeof(IEndpointDefinition).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                    .Select(Activator.CreateInstance).Cast<IEndpointDefinition>()
                );
        }

        foreach (var endpointDefinition in endpointDefinitions)
        {
            endpointDefinition.DefineServices(builder);
        }

        builder.Services.AddSingleton(endpointDefinitions as IReadOnlyCollection<IEndpointDefinition>);
    }

    public static void UseEndpointDefinitions(this WebApplication app, List<DefineApiVersion> defineApiVersions)
    {
        var apiVersionSet = app.ApiVersionSet(defineApiVersions ?? new List<DefineApiVersion>());

        var definitions = app.Services.GetRequiredService<IReadOnlyCollection<IEndpointDefinition>>();

        foreach (var endpointDefinition in definitions.Reverse())
        {
            endpointDefinition.DefineEndpoints(new AppBuilderDefinition { App = app, ApiVersionSet = apiVersionSet });
        }
    }
}