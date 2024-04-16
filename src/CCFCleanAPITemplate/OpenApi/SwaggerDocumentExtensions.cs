using CCFClean.Swagger;
using CCFClean.Swagger.Configurations;

namespace CCFCleanAPITemplate.OpenApi;

internal static class SwaggerDocumentExtensions
{
	internal static IServiceCollection AddCCFSwaggerConfig(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddCCFSwagger(opt =>
		 {
			 opt.OpenApiInfoExt = new OpenApiInfoExt
			 {
				 Title = "CCFCleanAPITemplate",
				 Description = "Swagger documentation for an CCFCleanAPITemplate api",
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
		 });
		return services;
	}

	internal static WebApplication UseCCFSwaggerConfig(this WebApplication app)
	{
		app.UseCCFSwagger(opt =>
		{
			opt.DocumentTitle = "CCFCleanAPITemplate";
			opt.ModelSchemaHide = false;
		});

		return app;
	}
}
