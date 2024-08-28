namespace CCFCleanAPITemplate.Authentication;

public class ApiKeyAuthenticationFilter(
	IConfiguration configuration)
	: IEndpointFilter
{
	private readonly IConfiguration _configuration = configuration;

	public async ValueTask<object?> InvokeAsync(
		EndpointFilterInvocationContext context,
		EndpointFilterDelegate next)
	{
		if (!context.HttpContext.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeaderName, out
				var extractedApiKey))
		{
			return new UnauthorizedHttpObjectResult("API Key missing");
		}

		var apiKey = _configuration.GetValue<string>(AuthConstants.ApiKeySectionName) ?? string.Empty;
		if (!apiKey.Equals(extractedApiKey))
		{
			return new UnauthorizedHttpObjectResult("Invalid API Key");
		}

		return await next(context);
	}
}
