using Asp.Versioning;
using CCFCleanAPITemplate.EndpointDefinition;
using CCFCleanAPITemplate.EndpointDefinition.Models;

namespace CCFCleanAPITemplate.Versioning.EndpointDefinition;

public class ApiVersioningEndpointDefinition : IEndpointDefinition
{
    public void DefineEndpoints(AppBuilderDefinition builderDefination)
    {

    }

    public void DefineServices(WebApplicationBuilder builder)
    {
        ApiVersionReaderEnum apiVersionReaderEnum = ApiVersionReaderEnum.UrlSegment;

        builder.Services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = apiVersionReaderEnum switch
            {
                ApiVersionReaderEnum.UrlSegment => new UrlSegmentApiVersionReader(),
                ApiVersionReaderEnum.QueryString => new QueryStringApiVersionReader("version"),
                ApiVersionReaderEnum.Header => new HeaderApiVersionReader("x-api-version"),
                _ => new UrlSegmentApiVersionReader(),
            };
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            if (apiVersionReaderEnum == ApiVersionReaderEnum.UrlSegment)
                options.SubstituteApiVersionInUrl = true;
        });
    }
}