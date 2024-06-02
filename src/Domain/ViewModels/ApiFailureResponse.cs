using System.Text.Json.Serialization;

namespace Domain.ViewModels;

public record ApiFailureResponse
{
	[JsonPropertyName("title")]
	public string? Title { get; set; }

	[JsonPropertyName("status")]
	public string? Status { get; set; }

	[JsonPropertyName("instance")]
	public string? Instance { get; set; }

	[JsonPropertyName("errorCode")]
	public string? ErrorCode { get; set; }

	[JsonPropertyName("traceId")]
	public string? TraceId { get; set; }

	[JsonPropertyName("Detail")]
	public string? Detail { get; set; }

	[JsonPropertyName("errorDescription")]
	public IDictionary<string, object?>? ErrorDescription { get; set; }
}