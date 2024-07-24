using Domain.Configs;
using CCFClean.Minimal.Definition;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CCFCleanAPITemplate.Authentication.Definitions;

public class OAuthDefinition : IEndpointDefinition
{
	public void DefineEndpoints(AppBuilderDefinition builderDefination)
	{
		builderDefination.App
			.AuthExceptionHandler()
			.UseAuthentication()
			.UseAuthorization();
	}

	public void DefineServices(WebApplicationBuilder builder)
	{
		builder.Services
				  .AddOptions<OAuthConfig>()
				  .Bind(builder.Configuration.GetSection(OAuthConfig.SectionName))
				  .ValidateDataAnnotations();

		var serviceProvider = builder.Services.BuildServiceProvider();
		var OauthConfig = serviceProvider.GetRequiredService<IOptions<OAuthConfig>>().Value;

		builder.Services
			.AddAuthentication("Bearer")
			.AddJwtBearer("Bearer", options =>
			{
				options.Authority = OauthConfig.Authority;
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidAudience = OauthConfig.Audience,
					ValidateAudience = true
				};
			})
			.Services.AddAuthorizationBuilder()
			.AddPolicy("customPolicy", policy =>
			{
				policy.RequireClaim("scope", OauthConfig.Scope ?? string.Empty);
			});
	}
}