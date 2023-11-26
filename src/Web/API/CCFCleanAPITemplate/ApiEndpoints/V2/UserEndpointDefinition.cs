using MediatR;
using Asp.Versioning;
using Domain.Configs.User;
using Wrapper = Shared.Wrapper;
using CCFCleanAPITemplate.EndpointDefinition;
using CCFCleanAPITemplate.OpenApi.Summaries.User;
using Application.Features.Users.Queries.GetByAPI;
using CCFCleanAPITemplate.EndpointDefinition.Models;
using Application.Features.Users.Queries.ViewModels;

namespace CCFCleanAPITemplate.ApiEndpoints.V2;

public class UserEndpointDefinition : IEndpointDefinition
{
	public void DefineEndpoints(AppBuilderDefinition builderDefination)
	{
		var mapToApiVersion = new ApiVersion(2);

		builderDefination.App
			.MapGet("v{version:apiVersion}/user/users", Users)
			.GetUserFromApiEndpointSummary<Wrapper.Result<List<UserViewModel>>>()
			.WithApiVersionSet(builderDefination.ApiVersionSet)
			.MapToApiVersion(mapToApiVersion);
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