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
        bool isDefaultModelSchemaExpand = Convert.ToBoolean(builderDefination.App.Configuration["OpenApiConfig:IsDefaultModelSchemaExpand"]);
        builderDefination.App.UseSwagger()
            .UseSwaggerUI(options =>
            {
                options.DocumentTitle = "SwaggerUI";
                if (!isDefaultModelSchemaExpand)
                    options.DefaultModelsExpandDepth(-1);
                var descriptions = builderDefination.App.DescribeApiVersions();
                foreach (var description in descriptions.Reverse())
                {
                    var url = $"{description.GroupName}/swagger.json";
                    var name = description.GroupName.ToUpperInvariant();
                    options.SwaggerEndpoint(url, name);
                }
            });
    }

    public void DefineServices(WebApplicationBuilder builder)
    {
        var openApiConfig = builder.Configuration.GetSection("OpenApiConfig").Get<OpenApiConfig>() ?? new OpenApiConfig();
        builder.Services
            .AddSingleton(openApiConfig)
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
                if (openApiConfig.SecurityExt != null && openApiConfig.SecurityExt.IsSecured)
                    options.AddSwaggerSecurityDefination();
                options.DocumentFilter<SwaggerDocumentFilter>();
            });
    }
}