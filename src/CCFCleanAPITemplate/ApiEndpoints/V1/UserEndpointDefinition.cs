using MediatR;
using Asp.Versioning;
using OpenApi.Summaries.User;
using Wrapper = Shared.Wrapper;
using CCFClean.Minimal.Definition;
using Infrastructure.Respositories;
using Application.Interfaces.Repositories;
using Application.Features.Users.Queries.GetAll;
using Application.Features.Users.Queries.GetById;
using Application.Features.Users.Queries.ViewModels;
using Application.Features.Users.Commands.UpsertUser;
using Application.Features.Users.Commands.DeleteUser;

namespace CCFCleanAPITemplate.ApiEndpoints.V1;

public class UserEndpointDefinition : IEndpointDefinition
{
	public void DefineEndpoints(AppBuilderDefinition builderDefination)
	{
		var mapToApiVersion = new ApiVersion(1);

		builderDefination.RouteBuilder
			.MapGet("users", () => Users)
			.GetAllUserEndpointSummary<Wrapper.Result<List<UserViewModel>>>()
			.MapToApiVersion(mapToApiVersion);

		builderDefination.RouteBuilder
			.MapGet("users/{id}", GetUserById)
			.GetUserByIdEndpointSummary<Wrapper.Result<UserViewModel>>()
			.MapToApiVersion(mapToApiVersion);

		builderDefination.RouteBuilder
			.MapPost("users/upsert", UpsertUser)
			.UpsertUserEndpointSummary<Wrapper.Result<Guid>>()
			.MapToApiVersion(mapToApiVersion);

		builderDefination.RouteBuilder
		   .MapDelete("users/delete/{id}", DeleteUser)
		   .DeleteUserEndpointSummary<Wrapper.Result<Guid>>()
		   .MapToApiVersion(mapToApiVersion);
	}

	public void DefineServices(WebApplicationBuilder builder)
	{
		builder.Services
			.AddTransient<IUserRepository, UserRepository>();
	}

	private async Task<IResult> Users(IMediator mediator)
	{
		return Results.Ok(await mediator.Send(new GetAllUserQuery()));
	}

	private async Task<IResult> GetUserById(IMediator mediator, Guid id)
	{
		return Results.Ok(await mediator.Send(new GetUserByIdQuery() { Id = id }));
	}

	private async Task<IResult> UpsertUser(IMediator mediator, UpsertUserCommand upsertUserCommand)
	{
		return Results.Ok(await mediator.Send(upsertUserCommand));
	}

	private async Task<IResult> DeleteUser(IMediator mediator, Guid id)
	{
		return Results.Ok(await mediator.Send(new DeleteUserCommand() { Id = id }));
	}
}