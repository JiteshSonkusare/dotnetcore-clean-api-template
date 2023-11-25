using NLog;
using System.Net;
using System.Net.Mime;
using Shared.Extension;
using Domain.ViewModels;
using Application.Common.Exceptions;

namespace CCFCleanAPITemplate.Middlewares;

public abstract class AbstractResponseHandlerMiddleware
{
	private readonly RequestDelegate _next;
	private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

	public AbstractResponseHandlerMiddleware(RequestDelegate next) => _next = next;

	public abstract (HttpStatusCode code, string message) HandleResponse(Exception exception);

	public async Task InvokeAsync(HttpContext httpContext)
	{
		try
		{
			if (httpContext == null)
			{
				_logger.Error($"{nameof(httpContext)} is null");
				throw new ArgumentNullException(nameof(httpContext));
			}

			_logger.Info($"RequestUri:{httpContext.Request.Scheme}://{httpContext.Request.Host}{httpContext.Request.Path}  " +
						 $"Method: {httpContext.Request.Method}");

			await _next(httpContext);

			_logger.Info($"Response status code: {httpContext.Response.StatusCode}");
		}
		catch (ValidationException ex)
		{
			await HandleValidationFailureAsync(httpContext, ex);
		}
		catch (Exception ex)
		{
			await HandleApiFailureAsync(httpContext, ex);
		}
	}

	private async Task HandleApiFailureAsync(HttpContext context, Exception exception)
	{
		var response = context.Response;
		response.ContentType = MediaTypeNames.Application.Json;

		var (status, failureResponse) = HandleResponse(exception);
		response.StatusCode = (int)status;
		_logger.Error($"StatusCode: {status}, Error: {failureResponse}");
		await response.WriteAsJsonAsync(failureResponse);
	}

	private static async Task HandleValidationFailureAsync(HttpContext context, ValidationException exception)
	{
		var failures = new ApiFailureResponse
		{
			Source = "ValidationFailure",
			Status = StatusCodes.Status400BadRequest.ToString(),
			Message = exception.Message
		};

		if (exception.Errors is not null)
		{
			failures.Extensions["errors"] = exception.Errors;
		}

		context.Response.StatusCode = StatusCodes.Status400BadRequest;
		_logger.Error(failures.ToJson());

		await context.Response.WriteAsJsonAsync(failures);
	}
}