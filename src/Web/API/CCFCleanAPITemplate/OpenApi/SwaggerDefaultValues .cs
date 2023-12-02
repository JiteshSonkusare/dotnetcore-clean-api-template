using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using CCFCleanAPITemplate.EndpointDefinition.CustomAttributes;

namespace CCFCleanAPITemplate.OpenApi;

public class SwaggerDefaultValues : IOperationFilter
{
	public void Apply(OpenApiOperation operation, OperationFilterContext context)
	{
		var apiDescription = context.ApiDescription;

		operation.Deprecated = IsDeprecated(apiDescription) | apiDescription.IsDeprecated();

		foreach (var responseType in context.ApiDescription.SupportedResponseTypes)
		{
			var responseKey = responseType.IsDefaultResponse ? "default" : responseType.StatusCode.ToString();
			var response = operation.Responses[responseKey];

			foreach (var contentType in response.Content.Keys)
			{
				if (!responseType.ApiResponseFormats.Any(x => x.MediaType == contentType))
				{
					response.Content.Remove(contentType);
				}
			}
		}

		if (operation.Parameters == null)
		{
			return;
		}
		foreach (var parameter in operation.Parameters)
		{
			var description = apiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);

			parameter.Description ??= description.ModelMetadata?.Description;

			if (parameter.Schema.Default == null &&
				 description.DefaultValue != null &&
				 description.DefaultValue is not DBNull &&
				 description.ModelMetadata is ModelMetadata modelMetadata)
			{
				var json = System.Text.Json.JsonSerializer.Serialize(description.DefaultValue, modelMetadata.ModelType);
				parameter.Schema.Default = OpenApiAnyFactory.CreateFromJson(json);
			}

			parameter.Required |= description.IsRequired;
		}
	}

	private static bool IsDeprecated(ApiDescription apiDescription)
	{
		return apiDescription.ActionDescriptor.EndpointMetadata
			.OfType<EndpointDeprecatedAttribute>()
			.Any();
	}
}