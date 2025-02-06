using CCFClean.Minimal.Definition;
using CCFCleanMinimalApiLib.ApiKeyAuthentication;

namespace CCFCleanAPITemplate.Authentication.EndpointDefinition;

public class ApiKeyAuthDefinition : IEndpointDefinition
{
	public void DefineEndpoints(AppBuilderDefinition builderDefination)
	{
		builderDefination.App
			.AuthExceptionHandler()
			.UseAuthorization();
	}

	public void DefineServices(WebApplicationBuilder builder)
	{
		builder.Services.ConfigureApiKeyAuthentication(
			builder.Configuration,
			ApiKeyAuthenticationConstants.DefaultSectionName)
		.AddAuthorizationBuilder();

		// Or

		//builder.Services.ConfigureApiKeyAuthentication(options =>
		//{
		//	options.WithHeaderName("x-api-key")
		//	       .WithApiKey("secret-api-key");
		//})
		//.AddAuthorizationBuilder();
	}
}