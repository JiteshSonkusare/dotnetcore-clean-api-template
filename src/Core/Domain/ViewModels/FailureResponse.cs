using System.Text.Json.Serialization;

namespace Domain.ViewModels;

public class FailureResponse
{
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("error")]
    public string? Error { get; set; }

    [JsonPropertyName("error_description")]
    public string? ErrorDescription { get; set; }

    [JsonExtensionData]
    public IDictionary<string, object?> Extensions { get; set; } = new Dictionary<string, object?>(StringComparer.Ordinal);

    public override string ToString() => $"type:{Type}|status:{Status}|error:{Error}|error_description:{ErrorDescription}|extensions:{Extensions}";
}