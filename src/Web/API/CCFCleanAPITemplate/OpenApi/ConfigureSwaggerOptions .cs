using System.Text;
using Microsoft.OpenApi.Models;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using CCFCleanAPITemplate.OpenApi.Configurations.Contracts;

namespace CCFCleanAPITemplate.OpenApi;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
	private readonly OpenApiConfig _openApiConfig;
	private readonly IApiVersionDescriptionProvider _provider;

	public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider, IOptions<OpenApiConfig> openApiConfig)
	{
		_provider = provider;
		_openApiConfig = openApiConfig.Value;
	}

	public void Configure(SwaggerGenOptions options)
	{
		foreach (var description in _provider.ApiVersionDescriptions)
		{
			options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
		}

		if (_openApiConfig.SecurityExt != null && _openApiConfig.SecurityExt.IsSecured)
			options.AddSwaggerSecurityDefination();
	}

	private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
	{
		var text = new StringBuilder(_openApiConfig.OpenApiInfoExt?.Description);
		var info = new OpenApiInfo()
		{
			Title = !string.IsNullOrWhiteSpace(_openApiConfig.OpenApiInfoExt?.Title) ? _openApiConfig.OpenApiInfoExt?.Title : "CCFCleanAPITemplate",
			Version = description.ApiVersion.ToString(),
			Contact = new OpenApiContact
			{
				Name = _openApiConfig?.OpenApiInfoExt?.OpenApiContactExt?.Name,
				Email = _openApiConfig?.OpenApiInfoExt?.OpenApiContactExt?.Email,
				Url = new Uri(_openApiConfig?.OpenApiInfoExt?.OpenApiContactExt?.Url ?? string.Empty, UriKind.Relative)
			}
		};

		if (description.IsDeprecated)
		{
			text.Append("This API version has been deprecated.");
		}

		// Sunset policy refers to the period of time during which a particular version of an API specification or
		// Swagger UI is no longer supported.
		if (description.SunsetPolicy is { } policy)
		{
			if (policy.Date is { } when)
			{
				text.Append(" The API will be sunset on ")
					.Append(when.Date.ToShortDateString())
					.Append('.');
			}

			if (policy.HasLinks)
			{
				text.AppendLine();

				foreach (var link in policy.Links)
				{
					if (link.Type != "text/html") continue;
					text.AppendLine();

					if (link.Title.HasValue)
					{
						text.Append(link.Title.Value).Append(": ");
					}

					text.Append(link.LinkTarget.OriginalString);
				}
			}
		}

		info.Description = !string.IsNullOrWhiteSpace(text.ToString()) ? text.ToString() : "Swagger documentation for an API.";
		return info;
	}
}