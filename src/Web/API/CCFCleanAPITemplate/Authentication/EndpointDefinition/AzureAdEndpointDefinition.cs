using Microsoft.Identity.Web;
using CCFCleanAPITemplate.EndpointDefinition;
using CCFCleanAPITemplate.EndpointDefinition.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CCFCleanAPITemplate.Authentication.EndpointDefinition;

[DeprecateEndpointDefinition("Azure AD is not in use")]
public class AzureAdEndpointDefinition : IEndpointDefinition
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
		builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
						.AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));
	}
}