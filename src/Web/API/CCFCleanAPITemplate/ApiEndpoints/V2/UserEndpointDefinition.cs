using MediatR;
using Asp.Versioning;
using Domain.Configs.User;
using Wrapper = Shared.Wrapper;
using CCFCleanAPITemplate.EndpointDefinition;
using CCFCleanAPITemplate.OpenApi.Summaries.User;
using Application.Features.Users.Queries.GetByAPI;
using CCFCleanAPITemplate.EndpointDefinition.Models;
using Application.Features.Users.Queries.ViewModels;
using CCFCleanAPITemplate.EndpointDefinition.CustomAttributes;

namespace CCFCleanAPITemplate.ApiEndpoints.V2;

public class UserEndpointDefinition : IEndpointDefinition
{
	public void DefineEndpoints(AppBuilderDefinition builderDefination)
	{
		var mapToApiVersion = new ApiVersion(2);

		builderDefination.App
			.MapGet("v{version:apiVersion}/users", Users)
			.GetUserFromApiEndpointSummary<Wrapper.Result<List<UserViewModel>>>()
			.WithApiVersionSet(builderDefination.ApiVersionSet)
			.MapToApiVersion(mapToApiVersion);

		builderDefination.App
			.MapGet("v{version:apiVersion}/users/{id}", GetUserById)
			.WithMetadata(new EndpointDeprecateAttribute("This endpoint is Deprecated"))
			.GetUserbyIdFromApiEndpointSummary<Wrapper.Result<UserViewModel>>()
			.WithApiVersionSet(builderDefination.ApiVersionSet)
			.MapToApiVersion(mapToApiVersion);
	}

	public void DefineServices(WebApplicationBuilder builder)
	{
		builder.Services.AddOptions<UserConfig>()
						.Bind(builder.Configuration.GetSection(nameof(UserConfig)))
						.ValidateDataAnnotations();
	}

	private async Task<IResult> Users(IMediator mediator)
	{
		return Results.Ok(await mediator.Send(new GetUserByAPIQuery()));
	}

	private IResult GetUserById(IMediator mediator)
	{
		return Results.Ok("Deprecated");
	}
}