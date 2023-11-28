using Domain.ViewModels;
using CCFCleanAPITemplate.OpenApi.Summaries.Base;

namespace CCFCleanAPITemplate.OpenApi.Summaries.User;

public static class UserEndpointAttributeSummaries
{
	public static RouteHandlerBuilder GetAllUserEndpointSummary<T>(this RouteHandlerBuilder endpoint) => endpoint.AddMetaData(
				   tag: "User",
				   summary: "Get all users",
				   description: "Use this api endpoint to get all users.",
				   responseAttributes: new List<SwaggerResponseAttributeExt>()
				   {
					   new(StatusCodes.Status200OK        , null, typeof(T)), 
					   new(StatusCodes.Status400BadRequest, null, typeof(ApplicationFailureResponse)),
					   new(StatusCodes.Status404NotFound  , null, typeof(ApplicationFailureResponse))
				   });

	public static RouteHandlerBuilder GetUserByIdEndpointSummary<T>(this RouteHandlerBuilder endpoint) => endpoint.AddMetaData(
				   tag: "User",
				   summary: "Get user by id",
				   description: "Use this api endpoint to get user by id.",
				   responseAttributes: new List<SwaggerResponseAttributeExt>()
				   {
					   new(StatusCodes.Status200OK        , null, typeof(T)),
					   new(StatusCodes.Status400BadRequest, null, typeof(ApplicationFailureResponse)),
					   new(StatusCodes.Status404NotFound  , null, typeof(ApplicationFailureResponse))
				   });

	public static RouteHandlerBuilder UpsertUserEndpointSummary<T>(this RouteHandlerBuilder endpoint) => endpoint.AddMetaData(
				   tag: "User",
				   summary: "Inser and update user",
				   description: "Use this api endpoint to insert and update user.",
				   responseAttributes: new List<SwaggerResponseAttributeExt>()
				   {
					   new(StatusCodes.Status200OK, null, typeof(T)),
					   new(StatusCodes.Status400BadRequest, "Exception", typeof(ApplicationFailureResponse)),
					   new(StatusCodes.Status404NotFound, null, typeof(ApplicationFailureResponse))
				   });

	public static RouteHandlerBuilder DeleteUserEndpointSummary<T>(this RouteHandlerBuilder endpoint) => endpoint.AddMetaData(
				   tag: "User",
				   summary: "Delete user",
				   description: "Use this api endpoint to delete user.",
				   responseAttributes: new List<SwaggerResponseAttributeExt>()
				   {
					   new(StatusCodes.Status200OK, null, typeof(T)),
					   new(StatusCodes.Status400BadRequest, null, typeof(ApplicationFailureResponse)),
					   new(StatusCodes.Status400BadRequest, "Validation Failure", typeof(ApiFailureResponse)),
					   new(StatusCodes.Status404NotFound, null, typeof(ApplicationFailureResponse))
				   });

	public static RouteHandlerBuilder GetUserFromApiEndpointSummary<T>(this RouteHandlerBuilder endpoint) => endpoint.AddMetaData(
				   tag: "User",
				   summary: "Get users from api call.",
				   description: "Use this api endpoint to get users from api call.",
				   responseAttributes: new List<SwaggerResponseAttributeExt>()
				   {
					   new(StatusCodes.Status200OK, null, typeof(T)),
					   new(StatusCodes.Status400BadRequest, null, typeof(ApplicationFailureResponse)),
					   new(StatusCodes.Status500InternalServerError, null, typeof(ApplicationFailureResponse)),
					   new(StatusCodes.Status404NotFound, null, typeof(ApplicationFailureResponse))
				   });
}