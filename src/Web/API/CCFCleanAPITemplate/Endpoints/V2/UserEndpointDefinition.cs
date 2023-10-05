using MediatR;
using Asp.Versioning;
using Wrapper = Shared.Wrapper;
using CCFCleanAPITemplate.EndpointDefinition;
using Application.Features.Users.Queries.GetAll;
using CCFCleanAPITemplate.OpenApi.Summaries.User;
using Application.Features.Users.Queries.GetById;
using Application.Features.Users.Queries.ViewModels;
using CCFCleanAPITemplate.EndpointDefinition.Models;

namespace CCFCleanAPITemplate.Endpoints.V2;

public class UserEndpointDefinition : IEndpointDefinition
{
    public void DefineEndpoints(AppBuilderDefinition builderDefination)
    {
        builderDefination.App
            .MapGet("v{version:apiVersion}/user/users", Users)
            .GetAllUserEndpointSummary<Wrapper.Result<List<UserViewModel>>>()
            .WithApiVersionSet(builderDefination.ApiVersionSet)
            .MapToApiVersion(new ApiVersion(2));

        builderDefination.App
            .MapGet("v{version:apiVersion}/user/{id}", GetUserById)
            .GetAllUserEndpointSummary<Wrapper.Result<UserViewModel>>()
            .WithApiVersionSet(builderDefination.ApiVersionSet)
            .MapToApiVersion(new ApiVersion(2));
    }

    public void DefineServices(WebApplicationBuilder builder)
    {

    }

    private async Task<IResult> Users(IMediator mediator)
    {
        return Results.Ok(await mediator.Send(new GetAllUserQuery()));
    }

    private async Task<IResult> GetUserById(IMediator mediator, Guid id)
    {
        return Results.Ok(await mediator.Send(new GetUserByIdQuery() { Id = id }));
    }
}