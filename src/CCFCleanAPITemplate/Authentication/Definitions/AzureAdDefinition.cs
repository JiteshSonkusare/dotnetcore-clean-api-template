using Microsoft.Identity.Web;
using CCFClean.Minimal.Definition;
using CCFClean.Minimal.Definition.CustomAttributes;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CCFCleanAPITemplate.Authentication.Definitions;

[EndpointDefinitionDeprecate("Definition Deprecated")]
public class AzureAdDefinition : IEndpointDefinition
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