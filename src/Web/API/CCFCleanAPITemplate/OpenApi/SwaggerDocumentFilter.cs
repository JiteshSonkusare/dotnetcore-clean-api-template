using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using CCFCleanAPITemplate.OpenApi.Configurations.Contracts;

namespace CCFCleanAPITemplate.OpenApi;

public class SwaggerDocumentFilter : IDocumentFilter
{
    private readonly OpenApiConfig _config;

    public SwaggerDocumentFilter(OpenApiConfig openApiConfig)
    {
        _config = openApiConfig;
    }

    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        if (_config.ServerPathFilters != null)
        {
            var isDevBasePath = _config.ServerPathFilters?.IsDevBasePath;

            if (isDevBasePath == null || Convert.ToBoolean(isDevBasePath))
                swaggerDoc.Servers = GetDevBasePathURLs();
            else
                swaggerDoc.Servers = GetBasePathURLs(_config?.ServerPathFilters?.URL ?? string.Empty, _config?.ServerPathFilters?.EnvironmentNames ?? new List<string>());
        }
        if (_config?.SecurityExt != null)
        {
            if (context.DocumentName == _config.SecurityExt?.NonSecuredVersion)
            {
                swaggerDoc.Components.SecuritySchemes.Remove("Bearer");
            }
        }
    }

    private List<OpenApiServer>? GetDevBasePathURLs()
    {
        var devBasePathFilters = _config.ServerPathFilters?.DevBasePathFilter;
        if (devBasePathFilters == null)
            return new List<OpenApiServer>();

        var openApiServers = devBasePathFilters?.Select(basePath =>
            new OpenApiServer
            {
                Description = basePath.Environment,
                Url = basePath.Url
            })
            .ToList();

        return openApiServers;
    }

    public static List<OpenApiServer> GetBasePathURLs(string url, List<string> envNames)
    {
        var serverVariables = new Dictionary<string, OpenApiServerVariable>
        {
            ["Environment"] = new OpenApiServerVariable
            {
                Default = envNames.FirstOrDefault(),
                Description = "Environment identifier.",
                Enum = envNames
            }
        };
        var server = new OpenApiServer
        {
            Url = url,
            Variables = serverVariables
        };
        return new List<OpenApiServer> { server };
    }
}