using Domain.ViewModels;
using CCFCleanAPITemplate.OpenApi.Summaries.Base;

namespace CCFCleanAPITemplate.OpenApi.Summaries.User;

public static class UserEndpointAttributeSummaries
{
    public static RouteHandlerBuilder GetAllUserEndpointSummary<T>(this RouteHandlerBuilder endpoint)
    {
        return endpoint.AddMetaData(
                   tag: "User",
                   summary: "Get all users",
                   description: "Use this api endpoint to get all users.",
                   responseAttributes: new List<SwaggerResponseAttributeExt>()
                   {
                       new SwaggerResponseAttributeExt(200, null, typeof(T)),
                       new SwaggerResponseAttributeExt(400, null, typeof(FailureResponse)),
                       new SwaggerResponseAttributeExt(404, null, typeof(FailureResponse))
                   });
    }
}
