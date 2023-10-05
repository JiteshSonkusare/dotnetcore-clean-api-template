using CCFCleanAPITemplate.EndpointDefinition.Models;

namespace CCFCleanAPITemplate.EndpointDefinition;

public interface IEndpointDefinition
{
    void DefineServices(WebApplicationBuilder Builder);

    void DefineEndpoints(AppBuilderDefinition builderDefination);
}