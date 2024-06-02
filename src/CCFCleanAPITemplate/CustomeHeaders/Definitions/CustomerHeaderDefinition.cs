using CCFClean.Minimal.Definition;
using Dotnet8CleanCodeAPI.CustomHeader;

namespace CCFCleanAPITemplate.CustomeHeaders.Definitions;

public class CustomerHeaderDefinition : IEndpointDefinition
{
	public void DefineEndpoints(AppBuilderDefinition app)
	{
		app.App.UseMiddleware<CustomHeaderMiddleware>();
	}

	public void DefineServices(WebApplicationBuilder builder)
	{
		builder.Services.AddScoped<IGlobalHeaders, GlobalHeaders>();
	}
}