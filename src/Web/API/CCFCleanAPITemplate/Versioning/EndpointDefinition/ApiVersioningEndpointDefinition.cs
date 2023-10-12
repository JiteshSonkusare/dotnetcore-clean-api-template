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
        ApiVersioningReaderEnum apiVersionReaderEnum = ApiVersioningReaderEnum.UrlSegment;

        builder.Services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = apiVersionReaderEnum switch
            {
                ApiVersioningReaderEnum.UrlSegment => new UrlSegmentApiVersionReader(),
                ApiVersioningReaderEnum.QueryString => new QueryStringApiVersionReader("version"),
                ApiVersioningReaderEnum.Header => new HeaderApiVersionReader("x-api-version"),
                _ => new UrlSegmentApiVersionReader(),
            };
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            if (apiVersionReaderEnum == ApiVersioningReaderEnum.UrlSegment)
                options.SubstituteApiVersionInUrl = true;
        });
    }
}