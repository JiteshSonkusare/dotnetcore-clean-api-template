using System.Net;
using System.Net.Mime;
using System.Text.Json;
using CCFCleanMinimalApiLib.ApiKeyAuthentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace CCFCleanAPITemplate.Authentication;

public static class AuthExceptionHandlerMiddleware
{
	public static IApplicationBuilder AuthExceptionHandler(this WebApplication app)
	{
		return app
			.Use(async (httpContext, next) =>
			{
				await next();
				if (httpContext.Response.StatusCode == (int)HttpStatusCode.Unauthorized || httpContext.Response.StatusCode == (int)HttpStatusCode.Forbidden)
				{
					var message = httpContext.Items.TryGetValue(ApiKeyAuthenticationConstants.UnauthorizedMessage, out
						var messageObj) ? messageObj : null;

					httpContext.Response.ContentType = MediaTypeNames.Application.ProblemJson;
					await httpContext.Response.WriteAsync(
						JsonSerializer.Serialize(
							CreateProblemDetails(httpContext, message)));
				}
			});
	}

	private static ProblemDetails CreateProblemDetails(in HttpContext httpContext, object? details = null)
	{
		var problemDetails = new ProblemDetails
		{
			Title = ReasonPhrases.GetReasonPhrase(httpContext.Response.StatusCode),
			Status = httpContext.Response.StatusCode,
			Instance = httpContext.Request.Path,
		};
		problemDetails.Extensions["traceId"] = httpContext.TraceIdentifier;
		problemDetails.Detail = details?.ToString() ?? "Invalid access token";

		return problemDetails;
	}
}