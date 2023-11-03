using System.Text.Json.Serialization;

namespace Domain.ViewModels;

public record ApiFailureResponse
{
	[JsonPropertyName("source")]
	public string? Source { get; set; }

	[JsonPropertyName("status")]
	public string? Status { get; set; }

	[JsonPropertyName("message")]
	public string? Message { get; set; }

	[JsonExtensionData]
	public IDictionary<string, object?> Extensions { get; set; } = new Dictionary<string, object?>(StringComparer.Ordinal);

	public override string ToString() => $"type:{Source}|status:{Status}|message:{Message}|extensions:{Extensions}";
}