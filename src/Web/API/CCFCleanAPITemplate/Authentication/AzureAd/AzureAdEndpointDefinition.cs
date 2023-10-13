using Microsoft.Identity.Web;
using CCFCleanAPITemplate.EndpointDefinition;
using CCFCleanAPITemplate.EndpointDefinition.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CCFCleanAPITemplate.Authentication.AzureAd;

public class AzureAdEndpointDefinition : IEndpointDefinition
{
    public void DefineEndpoints(AppBuilderDefinition builderDefination)
    {
        if (Convert.ToBoolean(builderDefination.App.Configuration["EnableAuthentication"]))
        {
            builderDefination.App
                .AuthExceptionHandler()
                .UseAuthentication()
                .UseAuthorization();
        }
    }

    public void DefineServices(WebApplicationBuilder builder)
    {
        if (Convert.ToBoolean(builder.Configuration["EnableAuthentication"]))
        {
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));
        }
    }
}