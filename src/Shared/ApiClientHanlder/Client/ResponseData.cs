using System.Net;
using Shared.Extension;
using System.Text.Json;
using System.Net.Http.Headers;

namespace Shared.ApiClientHanlder;

public sealed class ResponseData
{
    private readonly List<HeaderData> headers;

    public int StatusCode { get; private set; }

    public string Content { get; private set; }

    public IEnumerable<HeaderData> ResponseHeaders => headers;

    public ResponseData(HttpStatusCode statusCode, string content, IEnumerable<HeaderData>? responseHeaders)
    {
        StatusCode = (int)statusCode;
        Content = content;
        headers = responseHeaders?.ToList() ?? new List<HeaderData>();
    }

    public ResponseData(HttpStatusCode statusCode, string content, params HeaderData[]? responseHeaders)
        : this(statusCode, content, responseHeaders as IEnumerable<HeaderData>)
    {
    }

    public T? ConvertContent<T>(JsonSerializerOptions? options = null) => Content.ConvertFromJson<T?>(options);

    public object? ConvertContent(Type objectType, JsonSerializerOptions? options = null) => Content.ConvertFromJson(objectType, options);

	public T? ConvertXmlContent<T>() => Content.ConvertFromXml<T?>();

	internal ResponseData(HttpStatusCode statusCode, string content, HttpResponseHeaders responseHeaders, HttpResponseHeaders trailingHeaders)
    {
        StatusCode = (int)statusCode;
        Content = content;
        headers = new List<HeaderData>();
        if (responseHeaders?.Any() ?? false)
            headers.AddRange(responseHeaders.Select(H => new HeaderData(true, H.Key, H.Value.ToArray())));
        if (trailingHeaders?.Any() ?? false)
            headers.AddRange(trailingHeaders.Select(H => new HeaderData(true, H.Key, H.Value.ToArray())));
    }
}