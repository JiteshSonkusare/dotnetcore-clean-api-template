using MediatR;
using Asp.Versioning;
using Domain.Configs.User;
using Wrapper = Shared.Wrapper;
using CCFCleanAPITemplate.EndpointDefinition;
using CCFCleanAPITemplate.OpenApi.Summaries.User;
using Application.Features.Users.Queries.GetByAPI;
using CCFCleanAPITemplate.EndpointDefinition.Models;

namespace CCFCleanAPITemplate.Endpoints.V2;

public class UserEndpointDefinition : IEndpointDefinition
{
    public void DefineEndpoints(AppBuilderDefinition builderDefination)
    {
        builderDefination.App
            .MapGet("v{version:apiVersion}/user/users", Users)
            .GetUserFromApiEndpointSummary<Wrapper.Result<string>>()
            .WithApiVersionSet(builderDefination.ApiVersionSet)
            .MapToApiVersion(new ApiVersion(2));

    }

    public void DefineServices(WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton(new UserConfig
        {
            BaseURL = builder.Configuration["UserConfig:BaseURL"]
        });
    }

    private async Task<IResult> Users(IMediator mediator)
    {
        return Results.Ok(await mediator.Send(new GetUserByAPIQuery()));
    }
}