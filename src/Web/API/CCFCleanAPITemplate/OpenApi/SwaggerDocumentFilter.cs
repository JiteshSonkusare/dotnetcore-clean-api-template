using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using CCFCleanAPITemplate.OpenApi.Configurations.Contracts;

namespace CCFCleanAPITemplate.OpenApi;

public class SwaggerDocumentFilter : IDocumentFilter
{
	private readonly OpenApiConfig _openApiConfig;

	public SwaggerDocumentFilter(IOptions<OpenApiConfig> openApiConfig)
	{
		_openApiConfig = openApiConfig.Value;
	}

	public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
	{
		if (_openApiConfig.ServerPathFilters != null)
		{
			var isBasePathListFilter = _openApiConfig.ServerPathFilters?.IsBasePathListFilter;

			if (isBasePathListFilter == null || Convert.ToBoolean(isBasePathListFilter))
				swaggerDoc.Servers = GetBasePathListFilterURLs();
			else
				swaggerDoc.Servers = GetCustomeBasePathFilterURLs(_openApiConfig?.ServerPathFilters?.CustomeBasePathFilter?.URL ?? string.Empty, _openApiConfig?.ServerPathFilters?.CustomeBasePathFilter?.EnvironmentNames ?? new Enumerable.<string>());
		}
		if (_openApiConfig?.SecurityExt != null)
		{
			foreach (var _ in from nonSecuredVersion in _openApiConfig?.SecurityExt?.NonSecuredVersions
							  where context.DocumentName == nonSecuredVersion
							  select new { })
			{ swaggerDoc.Components.SecuritySchemes.Remove("Bearer"); }
		}
	}

	private List<OpenApiServer>? GetBasePathListFilterURLs()
	{
		var devBasePathFilters = _openApiConfig.ServerPathFilters?.BasePathListFilter;
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