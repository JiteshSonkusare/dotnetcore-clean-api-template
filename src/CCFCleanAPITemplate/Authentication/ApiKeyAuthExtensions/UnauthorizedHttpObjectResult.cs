namespace CCFCleanAPITemplate.Authentication;

public sealed class UnauthorizedHttpObjectResult(
	object body)
	: IResult,
	IStatusCodeHttpResult
{
	private readonly object _body = body;

	public int StatusCode => StatusCodes.Status401Unauthorized;

	int? IStatusCodeHttpResult.StatusCode => StatusCode;

	public Task ExecuteAsync(HttpContext httpContext)
	{
		ArgumentNullException.ThrowIfNull(httpContext);

		httpContext.Response.StatusCode = StatusCode;
		httpContext.Items["UnauthorizedMessage"] = _body;

		return Task.CompletedTask;
	}
}