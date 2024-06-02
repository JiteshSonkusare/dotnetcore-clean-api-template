using System.Reflection;
using Microsoft.OpenApi.Models;
using CCFClean.Minimal.CustomHeader;
using CCFCleanAPITemplate.CustomeHeaders;

namespace Dotnet8CleanCodeAPI.CustomHeader;

public class CustomHeaderMiddleware(RequestDelegate next)
{
	private readonly RequestDelegate _next = next;

	public async Task InvokeAsync(HttpContext context, IGlobalHeaders globalHeaders)
	{
		var headerProperties = typeof(GlobalHeaders).GetProperties()
			.Where(p => p.GetCustomAttribute<HeaderInfoAttribute>() != null)
			.Select(p => new { Property = p, HeaderInfo = p.GetCustomAttribute<HeaderInfoAttribute>() });

		foreach (var item in headerProperties)
		{
			if (item.HeaderInfo != null)
			{
				var value = GetValueFromContext(context, item.HeaderInfo.ParameterIn, item.HeaderInfo.Name);
				if (!string.IsNullOrEmpty(value))
				{
					globalHeaders.AddCustomHeader(item.Property.Name, value);
				}
			}
		}

		await _next(context);
	}

	private static string? GetValueFromContext(HttpContext context, ParameterLocation location, string name)
	{
		return location switch
		{
			ParameterLocation.Header => context.Request.Headers[name].FirstOrDefault(),
			ParameterLocation.Query => context.Request.Query[name].FirstOrDefault(),
			_ => string.Empty,
		};
	}
}