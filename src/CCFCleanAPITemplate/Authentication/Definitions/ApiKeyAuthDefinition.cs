using CCFClean.Minimal.Definition;
using CCFCleanAPITemplate.Authentication;

namespace DnbCustomerLookupPartnerApi.Authentication.EndpointDefinition;

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
		builder.Services.AddAuthorizationBuilder();
	}
}