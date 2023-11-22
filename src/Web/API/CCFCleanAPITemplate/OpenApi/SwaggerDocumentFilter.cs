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
            var isBasePathListFilter = _config.ServerPathFilters?.IsBasePathListFilter;

            if (isBasePathListFilter == null || Convert.ToBoolean(isBasePathListFilter))
                swaggerDoc.Servers = GetBasePathListFilterURLs();
            else
                swaggerDoc.Servers = GetCustomeBasePathFilterURLs(_config?.ServerPathFilters?.CustomeBasePathFilter?.URL ?? string.Empty, _config?.ServerPathFilters?.CustomeBasePathFilter?.EnvironmentNames ?? new List<string>());
        }
        if (_config?.SecurityExt != null)
        {
            foreach (var _ in from nonSecuredVersion in _config?.SecurityExt?.NonSecuredVersions
                              where context.DocumentName == nonSecuredVersion
                              select new { })
            { swaggerDoc.Components.SecuritySchemes.Remove("Bearer"); }
        }
    }

    private List<OpenApiServer>? GetBasePathListFilterURLs()
    {
        var devBasePathFilters = _config.ServerPathFilters?.BasePathListFilter;
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

    public static List<OpenApiServer> GetCustomeBasePathFilterURLs(string url, List<string> envNames)
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