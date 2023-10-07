using System.Text.Json.Serialization;

namespace Domain.ViewModels;

public record ValidationFailureResponse
{
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonExtensionData]
    public IDictionary<string, object?> Extensions { get; set; } = new Dictionary<string, object?>(StringComparer.Ordinal);

    public override string ToString() => $"type:{Type}|status:{Status}|message:{Message}|extensions:{Extensions}";
}
