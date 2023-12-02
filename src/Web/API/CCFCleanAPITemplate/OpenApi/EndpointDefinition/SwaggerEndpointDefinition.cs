using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http.Json;
using Swashbuckle.AspNetCore.SwaggerGen;
using CCFCleanAPITemplate.EndpointDefinition;
using CCFCleanAPITemplate.EndpointDefinition.Models;
using CCFCleanAPITemplate.OpenApi.Configurations.Contracts;

namespace CCFCleanAPITemplate.OpenApi.EndpointDefinition;

public class SwaggerEndpointDefinition : IEndpointDefinition
{
	public void DefineEndpoints(AppBuilderDefinition builderDefination)
	{
		builderDefination.App.UseSwagger()
			.UseSwaggerUI(options =>
			{
				options.DocumentTitle = "SwaggerUI";
				//options.DefaultModelsExpandDepth(-1);
				var descriptions = builderDefination.App.DescribeApiVersions().OrderByDescending(v => v.ApiVersion);
				foreach (var description in descriptions)
				{
					var url = $"{description.GroupName}/swagger.json";
					var name = description.GroupName.ToUpperInvariant();
					options.SwaggerEndpoint(url, name);
				}
			});
	}

	public void DefineServices(WebApplicationBuilder builder)
	{
		builder.Services
			.Configure<OpenApiConfig>(builder.Configuration.GetSection(nameof(OpenApiConfig)))
			.Configure<JsonOptions>(options =>
			{
				Shared.Extension.Extensions.SetGlobalJsonSerializerSettings(options.SerializerOptions);
			})
			.AddEndpointsApiExplorer()
			.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>()
			.AddSwaggerGen(options =>
			{
				options.EnableAnnotations();
				options.OperationFilter<SwaggerDefaultValues>();
				options.DocumentFilter<SwaggerDocumentFilter>();
			});
	}
}