using System.Text.Json;
using System.Xml.Serialization;
using System.Text.Json.Serialization;

namespace Shared.Extension;

public static class Extensions
{
    private static readonly JsonSerializerOptions serializerOptions;

    static Extensions()
    {
        serializerOptions = new JsonSerializerOptions();
    }

    public static string ToJson(this object obj)
    {
        return JsonSerializer.Serialize(obj, serializerOptions);
    }

    public static T? ConvertFromJson<T>(this string jsonContent)
    {
        if (string.IsNullOrWhiteSpace(jsonContent))
            return default;
        return JsonSerializer.Deserialize<T>(jsonContent, serializerOptions);
    }

    public static T? ConvertFromJson<T>(this string jsonContent, JsonSerializerOptions? customOptions = null)
    {
        if (string.IsNullOrWhiteSpace(jsonContent))
            return default;
        return JsonSerializer.Deserialize<T>(jsonContent, customOptions ?? globalJsonSerializerOptions);
    }

    public static object? ConvertFromJson(this string jsonContent, Type objectType, JsonSerializerOptions? customOptions = null)
    {
        if (string.IsNullOrWhiteSpace(jsonContent))
            return default;
        return JsonSerializer.Deserialize(jsonContent, objectType, customOptions ?? globalJsonSerializerOptions);
    }

    internal static JsonSerializerOptions? globalJsonSerializerOptions;

    public static void SetGlobalJsonSerializerSettings(JsonSerializerOptions jsonSerializerOptions, JsonIgnoreCondition jsonIgnoreCondition = JsonIgnoreCondition.Never)
    {
        globalJsonSerializerOptions = jsonSerializerOptions ?? throw new ArgumentNullException(nameof(jsonSerializerOptions), "Null Json serializer options are not allowed.");
        globalJsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        globalJsonSerializerOptions.DefaultIgnoreCondition = jsonIgnoreCondition;
        _ = globalJsonSerializerOptions.ToJson();
    }

	/// <summary>
	/// Convert xml to object
	/// </summary>
	/// <param name="xmlContent"></param>
	/// <param name="objectType"></param>
	/// <param name="customOptions"></param>
	/// <returns>Deserilized object data</returns>
	public static T? ConvertFromXml<T>(this string xmlContent)
	{
		if (string.IsNullOrWhiteSpace(xmlContent))
			return default;
		XmlSerializer serializer = new(typeof(T));
		using StringReader reader = new(xmlContent);
		return (T)serializer.Deserialize(reader)!;
	}
}