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

    public static RouteHandlerBuilder GetUserByIdEndpointSummary<T>(this RouteHandlerBuilder endpoint)
    {
        return endpoint.AddMetaData(
                   tag: "User",
                   summary: "Get user by id",
                   description: "Use this api endpoint to get user by id.",
                   responseAttributes: new List<SwaggerResponseAttributeExt>()
                   {
                       new SwaggerResponseAttributeExt(200, null, typeof(T)),
                       new SwaggerResponseAttributeExt(400, null, typeof(FailureResponse)),
                       new SwaggerResponseAttributeExt(400, "Validation Failure", typeof(ValidationFailureResponse)),
                       new SwaggerResponseAttributeExt(404, null, typeof(FailureResponse))
                   });
    }
    
    public static RouteHandlerBuilder UpsertUserEndpointSummary<T>(this RouteHandlerBuilder endpoint)
    {
        return endpoint.AddMetaData(
                   tag: "User",
                   summary: "Inser and update user",
                   description: "Use this api endpoint to insert and update user.",
                   responseAttributes: new List<SwaggerResponseAttributeExt>()
                   {
                       new SwaggerResponseAttributeExt(200, null, typeof(T)),
                       new SwaggerResponseAttributeExt(400, null, typeof(FailureResponse)),
                       new SwaggerResponseAttributeExt(400, "Validation Failure", typeof(ValidationFailureResponse)),
                       new SwaggerResponseAttributeExt(404, null, typeof(FailureResponse))
                   });
    }

    public static RouteHandlerBuilder DeleteUserEndpointSummary<T>(this RouteHandlerBuilder endpoint)
    {
        return endpoint.AddMetaData(
                   tag: "User",
                   summary: "Delete user",
                   description: "Use this api endpoint to delete user.",
                   responseAttributes: new List<SwaggerResponseAttributeExt>()
                   {
                       new SwaggerResponseAttributeExt(200, null, typeof(T)),
                       new SwaggerResponseAttributeExt(400, null, typeof(FailureResponse)),
                       new SwaggerResponseAttributeExt(400, "Validation Failure", typeof(ValidationFailureResponse)),
                       new SwaggerResponseAttributeExt(404, null, typeof(FailureResponse))
                   });
    }

    public static RouteHandlerBuilder GetUserFromApiEndpointSummary<T>(this RouteHandlerBuilder endpoint)
    {
        return endpoint.AddMetaData(
                   tag: "User",
                   summary: "Get users from api call.",
                   description: "Use this api endpoint to get users from api call..",
                   responseAttributes: new List<SwaggerResponseAttributeExt>()
                   {
                       new SwaggerResponseAttributeExt(200, null, typeof(T)),
                       new SwaggerResponseAttributeExt(400, null, typeof(FailureResponse)),
                       new SwaggerResponseAttributeExt(500, null, typeof(FailureResponse)),
                       new SwaggerResponseAttributeExt(404, null, typeof(FailureResponse))
                   });
    }
}