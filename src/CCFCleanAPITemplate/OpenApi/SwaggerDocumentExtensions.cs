﻿using CCFClean.Swagger;
using CCFClean.Swagger.Configurations;

namespace CCFCleanAPITemplate.OpenApi;

internal static class SwaggerDocumentExtensions
{
	/// <summary>
	/// Extended method implemenation from CCF Clean Minimal Swagger Definition.
	/// </summary>
	/// <param name="services"></param>
	/// <param name="configuration"></param>
	/// <returns></returns>
	internal static IServiceCollection AddCCFSwaggerExtenison(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddCCFSwagger(opt =>
		{
			opt.OpenApiInfoExt = new OpenApiInfoExt
			{
				Title = "CCFCleanAPITemplate",
				Description = "Swagger documentation for an CCFCleanAPITemplate API",
				OpenApiContactExt = new OpenApiContactExt()
				{
					Name = "Jitesh Sonkusare",
					Email = "jitesh.sonkusare@contoso.no",
					Url = new Uri("https://contoso.no")
				}
			};
			opt.SecurityExt = new SecurityExt
			{
				IsSecured = true,
				NonSecuredVersions = ["v1"]
			};
			opt.ServerPathFilters = configuration.GetSection("ServerPathFilters").Get<ServerPathFilters>();
			opt.EnableGlobalHeader = true;
		});
		return services;
	}

	/// <summary>
	/// Extended method implementation from the CCF Clean Minimal Swagger Definition.
	/// </summary>
	/// <param name="app"></param>
	/// <returns></returns>
	internal static WebApplication UseCCFSwaggerExtension(this WebApplication app)
	{
		app.UseCCFSwagger(opt =>
		{
			opt.DocumentTitle = "CCFCleanAPITemplate";
			opt.ModelSchemaHide = false;
		});

		return app;
	}
}
