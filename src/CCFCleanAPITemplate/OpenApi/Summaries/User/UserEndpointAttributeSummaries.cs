using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using CCFClean.Swagger.Summaries;

namespace OpenApi.Summaries.User;

public static class UserEndpointAttributeSummaries
{
	private const string _tagName = "User";

	public static RouteHandlerBuilder GetAllUserEndpointSummary<T>(this RouteHandlerBuilder endpoint) => endpoint.AddMetaData(
				   tag: _tagName,
				   summary: "Get all users",
				   description: "Use this api endpoint to get all users.",
				   responseAttributes: new List<SwaggerResponseAttributeExt>()
				   {
					   new(StatusCodes.Status200OK        , null, typeof(T)),
					   new(StatusCodes.Status404NotFound  , null, typeof(T)),
					   new(StatusCodes.Status400BadRequest, null, typeof(ApiFailureResponse)),
					   new(StatusCodes.Status500InternalServerError, null, typeof(ApiFailureResponse))
				   });

	public static RouteHandlerBuilder GetUserByIdEndpointSummary<T>(this RouteHandlerBuilder endpoint) => endpoint.AddMetaData(
				   tag: _tagName,
				   summary: "Get user by id",
				   description: "Use this api endpoint to get user by id.",
				   responseAttributes: new List<SwaggerResponseAttributeExt>()
				   {
					   new(StatusCodes.Status200OK        , null, typeof(T)),
					   new(StatusCodes.Status404NotFound  , null, typeof(T)),
					   new(StatusCodes.Status400BadRequest, null, typeof(ApiFailureResponse))
				   });

	public static RouteHandlerBuilder CreateUserEndpointSummary<T>(this RouteHandlerBuilder endpoint) => endpoint.AddMetaData(
				   tag: _tagName,
				   summary: "Create user",
				   description: "Use this api endpoint to create user.",
				   responseAttributes: new List<SwaggerResponseAttributeExt>()
				   {
					   new(StatusCodes.Status204NoContent, null, typeof(T)),
					   new(StatusCodes.Status400BadRequest, "Exception", typeof(ApiFailureResponse))
				   });

	public static RouteHandlerBuilder UdpateUserEndpointSummary<T>(this RouteHandlerBuilder endpoint) => endpoint.AddMetaData(
				   tag: _tagName,
				   summary: "Update user",
				   description: "Use this api endpoint to update user.",
				   responseAttributes: new List<SwaggerResponseAttributeExt>()
				   {
					   new(StatusCodes.Status204NoContent, null, null),
					   new(StatusCodes.Status404NotFound  , null, typeof(T)),
					   new(StatusCodes.Status400BadRequest, "Exception", typeof(ApiFailureResponse))
				   });

	public static RouteHandlerBuilder DeleteUserEndpointSummary<T>(this RouteHandlerBuilder endpoint) => endpoint.AddMetaData(
				   tag: _tagName,
				   summary: "Delete user",
				   description: "Use this api endpoint to delete user.",
				   responseAttributes: new List<SwaggerResponseAttributeExt>()
				   {
					   new(StatusCodes.Status204NoContent, null, null),

					   new(StatusCodes.Status400BadRequest, null, typeof(ProblemDetails))
				   });

	public static RouteHandlerBuilder GetUserFromApiEndpointSummary<T>(this RouteHandlerBuilder endpoint) => endpoint.AddMetaData(
				   tag: _tagName,
				   summary: "Get users from api call.",
				   description: "Use this api endpoint to get users from api call.",
				   responseAttributes: new List<SwaggerResponseAttributeExt>()
				   {
					   new(StatusCodes.Status200OK, null, typeof(T)),
					   new(StatusCodes.Status400BadRequest, null, typeof(ApiFailureResponse)),
					   new(StatusCodes.Status500InternalServerError, null, typeof(ApiFailureResponse))
				   });

	public static RouteHandlerBuilder GetUserbyIdFromApiEndpointSummary<T>(this RouteHandlerBuilder endpoint) => endpoint.AddMetaData(
				   tag: _tagName,
				   summary: "Get user by id from api call.",
				   description: "Use this api endpoint to get user by id from api call.",
				   responseAttributes: new List<SwaggerResponseAttributeExt>()
				   {
					   new(StatusCodes.Status200OK, null, typeof(T)),
					   new(StatusCodes.Status400BadRequest, null, typeof(ApiFailureResponse)),
					   new(StatusCodes.Status500InternalServerError, null, typeof(ApiFailureResponse))
				   });
}