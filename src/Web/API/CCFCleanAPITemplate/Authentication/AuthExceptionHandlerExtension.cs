using System.Net;
using System.Net.Mime;
using System.Text.Json;
using Domain.ViewModels;

namespace CCFCleanAPITemplate.Authentication;

public static class AuthExceptionHandlerExtension
{
    public static IApplicationBuilder AuthExceptionHandler(this WebApplication app)
    {
		return app
			.Use(async (httpContext, next) =>
			{
				await next();
				if (httpContext.Response.StatusCode == (int)HttpStatusCode.Unauthorized || httpContext.Response.StatusCode == (int)HttpStatusCode.Forbidden)
				{
					httpContext.Response.ContentType = MediaTypeNames.Application.Json;
					await httpContext.Response.WriteAsync(JsonSerializer.Serialize(
					new ApiFailureResponse()
					{
						Status = httpContext.Response.StatusCode.ToString(),
						Source = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}{httpContext.Request.Path}",
						Message = "Invalid access token."
					}).ToString());
				}
			});
	}
}