using MediatR;
using Asp.Versioning;
using Domain.Configs.User;
using OpenApi.Summaries.User;
using Wrapper = Shared.Wrapper;
using CCFClean.Minimal.Definition;
using Application.Features.Users.Queries.GetByAPI;
using Application.Features.Users.Queries.ViewModels;
using CCFClean.Minimal.Definition.CustomAttributes;

namespace CCFCleanAPITemplate.ApiEndpoints.V2;

public class UserEndpointDefinition : IEndpointDefinition
{
	public void DefineEndpoints(AppBuilderDefinition builderDefination)
	{
		var mapToApiVersion = new ApiVersion(2);

		builderDefination.RouteBuilder
			.MapGet("users", Users)
			.GetUserFromApiEndpointSummary<Wrapper.Result<List<UserViewModel>>>()
			.MapToApiVersion(mapToApiVersion)
			.RequireAuthorization();

		builderDefination.RouteBuilder
			.MapGet("users/{id}", GetUserById)
			.WithMetadata(new EndpointDeprecateAttribute("This endpoint is Deprecated"))
			.GetUserbyIdFromApiEndpointSummary<Wrapper.Result<UserViewModel>>()
			.MapToApiVersion(mapToApiVersion)
			.RequireAuthorization();
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