using System.Text.Json.Serialization;

namespace Domain.ViewModels;

public record ApplicationFailureResponse
{
    /// <summary>
    /// HttpStatus
    /// </summary>
    [JsonPropertyName("status")]
    public int? Status { get; set; }

    /// <summary>
    /// Exception Source
    /// </summary>
    [JsonPropertyName("source")]
    public string? Source { get; set; }

    /// <summary>
    /// Exception Message
    /// </summary>
    [JsonPropertyName("error")]
    public string? Error { get; set; }

    /// <summary>
    /// Exception StackTrace
    /// </summary>
    [JsonPropertyName("error_description")]
    public string? ErrorDescription { get; set; }

    public override string ToString() => $"status:{Status}|source:{Source}|error:{Error}|error_description:{ErrorDescription}";
}