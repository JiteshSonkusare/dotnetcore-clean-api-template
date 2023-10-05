using Swashbuckle.AspNetCore.Annotations;

namespace CCFCleanAPITemplate.OpenApi.Summaries.Base;

public record SwaggerResponseAttributeExt(int HttpStatusCode, string? Description, Type? Type);

public static class MinimalAttributeExtensions
{
    /// <summary>
    /// Use this method to customize swagger endpoints with tag, summary, description and response types.
    /// </summary>
    /// <param name="endpoint"></param>
    /// <param name="tag"></param>
    /// <param name="summary"></param>
    /// <param name="description"></param>
    /// <param name="responseAttributes"></param>
    /// <returns></returns>
    public static RouteHandlerBuilder AddMetaData(this RouteHandlerBuilder endpoint, string tag, string? summary = null, string? description = null, List<SwaggerResponseAttributeExt>? responseAttributes = null)
    {
        endpoint.WithTags(tag);
        endpoint.WithMetadata(new SwaggerOperationAttribute(summary, description));

        if (responseAttributes != null)
        {
            foreach (var attribute in responseAttributes)
            {
                Type responseType = attribute.Type ?? typeof(void);
                endpoint.WithMetadata(new SwaggerResponseAttribute(statusCode: attribute.HttpStatusCode, description: attribute.Description, type: responseType));
            }
        }

        return endpoint;
    }
}