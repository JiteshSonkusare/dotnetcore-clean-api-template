using MediatR;
using Asp.Versioning;
using Domain.Configs.User;
using OpenApi.Summaries.User;
using Infrastructure.Services;
using Wrapper = Shared.Wrapper;
using CCFClean.Minimal.Definition;
using Application.Interfaces.Services;
using Application.Features.Users.Dtos;
using Application.Features.Users.Queries;
using CCFCleanAPITemplate.Authentication;
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

	// Register dependencies in DI related to this class/functionality.
	public void DefineServices(WebApplicationBuilder builder)
	{
		builder.Services
			.AddTransient<IUserService, UserService>()
			.AddOptions<UserConfig>()
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
		.AddEndpointFilter<ApiKeyAuthenticationFilter>(); // Use for Apikey authentication
		//.Authorize("customPolicy"); // Use this for OAuht and AzureAd authentication
	}

	private static void GetById(IEndpointRouteBuilder endpoint, ApiVersion mapToApiVersion)
	{
		endpoint.MapGet("users/{id:guid}", async (ISender sender) =>
		{
			var user = await sender.Send(new GetUserByAPIQuery());
			return Results.Ok("Deprecated");
		})
		.WithMetadata(new ApiEndpointDeprecate("This endpoint is Deprecated"))
		.GetUserbyIdFromApiEndpointSummary<Wrapper.Result<UserDto>>()
		.MapToApiVersion(mapToApiVersion)
		.AddEndpointFilter<ApiKeyAuthenticationFilter>(); // Use for Apikey authentication
		//.Authorize("customPolicy"); // Use this for OAuht and AzureAd authentication
	}
}