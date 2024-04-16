using Domain.ViewModels;
using CCFClean.Swagger.Summaries;

namespace OpenApi.Summaries.User;

public static class UserEndpointAttributeSummaries
{
	public static RouteHandlerBuilder GetAllUserEndpointSummary<T>(this RouteHandlerBuilder endpoint) => endpoint.AddMetaData(
				   tag: "User",
				   summary: "Get all users",
				   description: "Use this api endpoint to get all users.",
				   responseAttributes: new List<SwaggerResponseAttributeExt>()
				   {
					   new(StatusCodes.Status200OK        , null, typeof(T)),
					   new(StatusCodes.Status400BadRequest, null, typeof(ApiFailureResponse))
				   });

	public static RouteHandlerBuilder GetUserByIdEndpointSummary<T>(this RouteHandlerBuilder endpoint) => endpoint.AddMetaData(
				   tag: "User",
				   summary: "Get user by id",
				   description: "Use this api endpoint to get user by id.",
				   responseAttributes: new List<SwaggerResponseAttributeExt>()
				   {
					   new(StatusCodes.Status200OK        , null, typeof(T)),
					   new(StatusCodes.Status400BadRequest, null, typeof(ApiFailureResponse))
				   });

	public static RouteHandlerBuilder UpsertUserEndpointSummary<T>(this RouteHandlerBuilder endpoint) => endpoint.AddMetaData(
				   tag: "User",
				   summary: "Inser and update user",
				   description: "Use this api endpoint to insert and update user.",
				   responseAttributes: new List<SwaggerResponseAttributeExt>()
				   {
					   new(StatusCodes.Status200OK, null, typeof(T)),
					   new(StatusCodes.Status400BadRequest, "Exception", typeof(ApiFailureResponse))
				   });

	public static RouteHandlerBuilder DeleteUserEndpointSummary<T>(this RouteHandlerBuilder endpoint) => endpoint.AddMetaData(
				   tag: "User",
				   summary: "Delete user",
				   description: "Use this api endpoint to delete user.",
				   responseAttributes: new List<SwaggerResponseAttributeExt>()
				   {
					   new(StatusCodes.Status200OK, null, typeof(T)),
					   new(StatusCodes.Status400BadRequest, null, typeof(ApiFailureResponse))
				   });

	public static RouteHandlerBuilder GetUserFromApiEndpointSummary<T>(this RouteHandlerBuilder endpoint) => endpoint.AddMetaData(
				   tag: "User",
				   summary: "Get users from api call.",
				   description: "Use this api endpoint to get users from api call.",
				   responseAttributes: new List<SwaggerResponseAttributeExt>()
				   {
					   new(StatusCodes.Status200OK, null, typeof(T)),
					   new(StatusCodes.Status400BadRequest, null, typeof(ApiFailureResponse)),
					   new(StatusCodes.Status500InternalServerError, null, typeof(ApiFailureResponse))
				   });

	public static RouteHandlerBuilder GetUserbyIdFromApiEndpointSummary<T>(this RouteHandlerBuilder endpoint) => endpoint.AddMetaData(
				   tag: "User",
				   summary: "Get user by id from api call.",
				   description: "Use this api endpoint to get user by id from api call.",
				   responseAttributes: new List<SwaggerResponseAttributeExt>()
				   {
					   new(StatusCodes.Status200OK, null, typeof(T)),
					   new(StatusCodes.Status400BadRequest, null, typeof(ApiFailureResponse)),
					   new(StatusCodes.Status500InternalServerError, null, typeof(ApiFailureResponse))
				   });
}