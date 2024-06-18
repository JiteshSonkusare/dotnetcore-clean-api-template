using CCFClean.Minimal.Definition;
using CCFClean.Minimal.CustomHeader;
using CCFCleanAPITemplate.CustomHeader;

namespace CCFCleanAPITemplate.CustomeHeaders.Definitions;

public class CustomerHeaderDefinition : IEndpointDefinition
{
	public void DefineEndpoints(AppBuilderDefinition app) { }

	public void DefineServices(WebApplicationBuilder builder)
	{
		builder.Services.AddScoped<IGlobalHeaders, GlobalHeaders>();
	}
}