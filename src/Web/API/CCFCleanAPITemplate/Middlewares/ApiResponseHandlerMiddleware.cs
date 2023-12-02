using System.Net;
using System.Text.Json;
using Domain.ViewModels;
using Application.Common.Exceptions;
using System.Security.Authentication;

namespace CCFCleanAPITemplate.Middlewares;

public class ApiResponseHandlerMiddleware : AbstractResponseHandlerMiddleware
{
    public ApiResponseHandlerMiddleware(RequestDelegate next) : base(next) { }

	public override (HttpStatusCode code, string message) HandleResponse(Exception exception)
	{
		var code = exception switch
		{
			AuthenticationException
				=> HttpStatusCode.Unauthorized,
			GeneralApplicationException
			or ArgumentNullException
			or NullReferenceException
			or TaskCanceledException
			or ApiException
				=> HttpStatusCode.BadRequest,
			InvalidOperationException
				=> HttpStatusCode.BadGateway,
			_
				=> HttpStatusCode.InternalServerError,
		};

		if (exception == null)
			return default!;

		var failures = new ApiFailureResponse
		{
			Source = exception.Source,
			Status = code.ToString(),
			Message = exception.Message ?? string.Empty,
		};

		if (exception.InnerException is not null)
		{
			failures.Extensions["errors"] = exception.InnerException.Message ?? string.Empty;
		}

		return (code, JsonSerializer.Serialize(failures));
	}
}