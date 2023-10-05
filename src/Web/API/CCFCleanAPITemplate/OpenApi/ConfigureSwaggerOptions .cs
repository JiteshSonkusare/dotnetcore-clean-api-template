using System.Text;
using Microsoft.OpenApi.Models;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using CCFCleanAPITemplate.OpenApi.Configurations.Contracts;

namespace CCFCleanAPITemplate.OpenApi;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly OpenApiConfig _config;
    private readonly IApiVersionDescriptionProvider _provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider, OpenApiConfig openApiConfigs)
    {
        _provider = provider;
        _config = openApiConfigs;
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
        }
    }

    private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        var text = new StringBuilder(_config.OpenApiInfoExt?.Description);
        var info = new OpenApiInfo()
        {
            Title = !string.IsNullOrWhiteSpace(_config.OpenApiInfoExt?.Title) ? _config.OpenApiInfoExt?.Title : "SwaggerUI",
            Version = description.ApiVersion.ToString(),
            Contact = new OpenApiContact
            {
                Name = _config?.OpenApiInfoExt?.OpenApiContactExt?.Name,
                Email = _config?.OpenApiInfoExt?.OpenApiContactExt?.Email
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
