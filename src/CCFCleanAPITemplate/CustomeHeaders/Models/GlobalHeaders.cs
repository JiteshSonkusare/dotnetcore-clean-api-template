using Microsoft.OpenApi.Models;
using CCFClean.Minimal.CustomHeader;
using Domain.Resources.GlobalHeaders;

namespace CCFCleanAPITemplate.CustomHeader;

public class GlobalHeaders : IGlobalHeaders
{
    private readonly Dictionary<string, string> data = new();

	public void AddCustomHeader(string propertyName, string value)
	{
		data[propertyName] = value;
	}

	[HeaderInfo(nameof(HeaderName.TraceId), "string", Description = "Custom correlation-id", DataFormat = "uuid")]
	public Guid? TraceId => data.ContainsKey(nameof(TraceId)) && Guid.TryParse(data[nameof(TraceId)], out var value) ? value : null;

    [HeaderInfo(nameof(HeaderName.Timestamp), "string", Description = "Timestamp", ParameterIn = ParameterLocation.Header, IsRequired = false)]
    public string? Timestamp => data.ContainsKey(nameof(Timestamp)) ? data[nameof(Timestamp)] : string.Empty;

	[HeaderInfo(nameof(HeaderName.ApplicationType), "string", Description = "Application type.", ParameterIn = ParameterLocation.Query, AllowedValues = "Client, Server", DefaultValue = "Server")]
	public string ApplicationType => data.ContainsKey(nameof(ApplicationType)) ? data[nameof(ApplicationType)] : string.Empty;
}