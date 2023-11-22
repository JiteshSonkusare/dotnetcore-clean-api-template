using System.Text.Json.Serialization;

namespace Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UserStatus
{
    Active = 0,
    Inactive = 1,
}
