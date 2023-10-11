using MediatR;
using Asp.Versioning;
using Wrapper = Shared.Wrapper;
using Infrastructure.Respositories;
using Application.Interfaces.Repositories;
using CCFCleanAPITemplate.EndpointDefinition;
using Application.Features.Users.Queries.GetAll;
using Application.Features.Users.Queries.GetById;
using CCFCleanAPITemplate.OpenApi.Summaries.User;
using CCFCleanAPITemplate.EndpointDefinition.Models;
using Application.Features.Users.Queries.ViewModels;
using Application.Features.Users.Commands.UpsertUser;
using Application.Features.Users.Commands.DeleteUser;

namespace CCFCleanAPITemplate.Endpoints.V1;

public class UserEndpointDefinition : IEndpointDefinition
{
    public void DefineEndpoints(AppBuilderDefinition builderDefination)
    {
        builderDefination.App
            .MapGet("v{version:apiVersion}/users", Users)
            .GetAllUserEndpointSummary<Wrapper.Result<List<UserViewModel>>>()
            .WithApiVersionSet(builderDefination.ApiVersionSet)
            .MapToApiVersion(new ApiVersion(1));

        builderDefination.App
            .MapGet("v{version:apiVersion}/users/{id}", GetUserById)
            .GetUserByIdEndpointSummary<Wrapper.Result<UserViewModel>>()
            .WithApiVersionSet(builderDefination.ApiVersionSet)
            .MapToApiVersion(new ApiVersion(1));

        builderDefination.App
            .MapPost("v{version:apiVersion}/users/upsert", UpsertUser)
            .UpsertUserEndpointSummary<Wrapper.Result<Guid>>()
            .WithApiVersionSet(builderDefination.ApiVersionSet)
            .MapToApiVersion(new ApiVersion(1));

        builderDefination.App
           .MapDelete("v{version:apiVersion}/users/delete/{id}", DeleteUser)
           .DeleteUserEndpointSummary<Wrapper.Result<Guid>>()
           .WithApiVersionSet(builderDefination.ApiVersionSet)
           .MapToApiVersion(new ApiVersion(1));
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