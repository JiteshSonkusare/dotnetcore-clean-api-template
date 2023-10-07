using CCFCleanAPITemplate.EndpointDefinition.Models;

namespace CCFCleanAPITemplate.EndpointDefinition;

public interface IEndpointDefinition
{
    void DefineServices(WebApplicationBuilder builder);

    void DefineEndpoints(AppBuilderDefinition builderDefination);
}