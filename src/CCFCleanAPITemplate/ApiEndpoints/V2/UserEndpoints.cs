using MediatR;
using Asp.Versioning;
using Domain.Configs.User;
using OpenApi.Summaries.User;
using Wrapper = Shared.Wrapper;
using CCFClean.Minimal.Definition;
using Application.Features.Users.Dtos;
using Application.Features.Users.Queries;
using CCFClean.Minimal.Definition.CustomAttributes;

namespace CCFCleanAPITemplate.ApiEndpoints.V2;

public class UserEndpoints : IEndpointDefinition
{
	public void DefineEndpoints(AppBuilderDefinition builderDefination)
	{
		var mapToApiVersion = new ApiVersion(2);

		GetAll(builderDefination.RouteBuilder, mapToApiVersion);
		GetById(builderDefination.RouteBuilder, mapToApiVersion);
	}

	public void DefineServices(WebApplicationBuilder builder)
	{
		builder.Services.AddOptions<UserConfig>()
						.Bind(builder.Configuration.GetSection(nameof(UserConfig)))
						.ValidateDataAnnotations();
	}

	private static void GetAll(IEndpointRouteBuilder endpoint, ApiVersion mapToApiVersion)
	{
		endpoint.MapGet("users", async (ISender sender) =>
		{
			return Results.Ok(await sender.Send(new GetUserByAPIQuery()));
		})
		.GetUserFromApiEndpointSummary<Wrapper.Result<List<UserDto>>>()
		.MapToApiVersion(mapToApiVersion)
		.RequireAuthorization();
	}

	private static void GetById(IEndpointRouteBuilder endpoint, ApiVersion mapToApiVersion)
	{
		endpoint.MapGet("users/{id:guid}", async (ISender sender) =>
		{
			var user = await sender.Send(new GetUserByAPIQuery());
			return Results.Ok("Deprecated");
		})
		.WithMetadata(new EndpointDeprecateAttribute("This endpoint is Deprecated"))
		.GetUserbyIdFromApiEndpointSummary<Wrapper.Result<UserDto>>()
		.MapToApiVersion(mapToApiVersion)
		.RequireAuthorization();
	}
}