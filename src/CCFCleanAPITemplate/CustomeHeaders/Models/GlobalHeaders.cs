using Microsoft.OpenApi.Models;
using CCFClean.Minimal.CustomHeader;

namespace CCFCleanAPITemplate.CustomHeader;

public class GlobalHeaders : IGlobalHeaders
{
	private readonly Dictionary<string, string> data = new();

	public void AddCustomHeader(string propertyName, string value)
	{
		data[propertyName] = value;
	}

	[HeaderInfo("X-TraceId", "string", Description = "Custom correlation-id", DataFormat = "uuid")]
	public Guid? TraceId => data.ContainsKey(nameof(TraceId)) && Guid.TryParse(data[nameof(TraceId)], out var value) ? value : null;

	[HeaderInfo("X-UserId", "string", Description = "Internal employee Id", IsRequired = true)]
	public string UserId => data.ContainsKey(nameof(UserId)) ? data[nameof(UserId)] : string.Empty;

	[HeaderInfo("X-UserType", "string", Description = "Internal user type.", ParameterIn = ParameterLocation.Query, AllowedValues = "Agent, System", DefaultValue = "System")]
	public string UserType => data.ContainsKey(nameof(UserType)) ? data[nameof(UserType)] : string.Empty;
}