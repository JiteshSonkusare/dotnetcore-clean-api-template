using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CCFCleanAPITemplate.Authentication;

public static class AzureAdServiceCollectionExtensions
{
    public static IServiceCollection RegisterAzureADDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddMicrosoftIdentityWebApi(configuration.GetSection("AzureAd"));
        return services;
    }
}