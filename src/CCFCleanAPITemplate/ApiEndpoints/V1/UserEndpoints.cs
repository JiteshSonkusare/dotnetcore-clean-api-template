using MediatR;
using Asp.Versioning;
using Shared.Wrapper;
using Microsoft.AspNetCore.Mvc;
using CCFClean.Minimal.Definition;
using Infrastructure.Respositories;
using CCFClean.Minimal.CustomHeader;
using Application.Features.Users.Dtos;
using CCFCleanAPITemplate.CustomHeader;
using Application.Features.Users.Commands;
using Application.Interfaces.Repositories;
using Application.Features.Users.Queries.GetAll;
using Application.Features.Users.Queries.GetById;
using CCFCleanAPITemplate.OpenApi.Summaries.User;

namespace CCFCleanAPITemplate.ApiEndpoints.V1;

public class UserEndpoints : IEndpointDefinition
{
	public void DefineEndpoints(AppBuilderDefinition builderDefination)
	{
		var mapToApiVersion = new ApiVersion(1);
		var endpoint = builderDefination.RouteBuilder;

		//Get: GetAll
		endpoint.MapGet("users", async (
			ISender sender) =>
		{
			var result = await sender.Send(new GetAllUserQuery());

			return result.Match(
				onSuccess: users => Results.Ok(users),
				onFailure: error => Results.NotFound(error));
		})
		.GetAllUserEndpointSummary<Result<List<UserDto>>>()
		.MapToApiVersion(mapToApiVersion);

		//Get: GetById
		endpoint.MapGet("users/{id:guid}", async (
			Guid id,
			ISender sender) =>
		{
			var result = await sender.Send(new GetUserByIdQuery(id));

			return result.Match(
				onSuccess: user => Results.Ok(user),
				onFailure: error => Results.NotFound(error));
		})
		.GetUserByIdEndpointSummary<Result<UserDto>>()
		.MapToApiVersion(mapToApiVersion);


		//Post: Create
		endpoint.MapPost("users/create", async (
			CreateUserRequest request,
			ISender sender,
			IGlobalHeaders globalHeaders,
			[FromHeader] string UserId) =>
		{
			var headers = (GlobalHeaders)globalHeaders;

			var command = new CreateUserCommand(
				request.FirstName,
				request.LastName,
                UserId,
				request.Mobile,
				request.Phone,
				request.Address,
				request.Gender,
				request.Status);

			var response = await sender.Send(command);

			return Results.Created();

		})
		.CreateUserEndpointSummary<Result<Guid>>()
		.MapToApiVersion(mapToApiVersion);


		//Put: Update
		endpoint.MapPut("users/update", async (
			[FromBody] UpdateUserRequest request,
			ISender sender,
			HttpContext httpContext,
            [FromHeader] string UserId) =>
		{
			var headers = httpContext.Items[typeof(GlobalHeaders)] as GlobalHeaders;

			var command = new UpdateUserCommand(
				request.Id,
				request.FirstName,
				request.LastName,
				UserId,
				request.Mobile,
				request.Phone,
				request.Address,
				request.Gender,
				request.Status);

			var result = await sender.Send(command);

			return result.Match(
				onSuccess: (id) => Results.NoContent(),
				onFailure: error => Results.NotFound(result));
		})
		.UdpateUserEndpointSummary<Result<Guid>>()
		.MapToApiVersion(mapToApiVersion);

		// Delete: Delete
		endpoint.MapDelete("users/delete/{id:guid}", async (
			Guid id,
			ISender sender) =>
		{
			var result = await sender.Send(new DeleteUserCommand(id));

			return result.Match(
				onSuccess: id => Results.NoContent(),
				onFailure: error => Results.NotFound(error));
		})
		.DeleteUserEndpointSummary<Result<Guid>>()
		.MapToApiVersion(mapToApiVersion);
	}

	// Register DI related to this class/functionality.
	public void DefineServices(WebApplicationBuilder builder)
	{
		builder.Services
			.AddTransient<IUserRepository, UserRepository>();
	}
}