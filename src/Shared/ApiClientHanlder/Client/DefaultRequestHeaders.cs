namespace Shared.ApiClientHanlder;

public record DefaultRequestHeaders
{
    public DefaultRequestHeaders(string[] acceptedMediaTypes, params HeaderData[] headers)
    {
        AcceptedMediaTypes = acceptedMediaTypes;
        Headers = headers;
    }

    public string[]? AcceptedMediaTypes { get; }
    public IEnumerable<HeaderData>? Headers { get; }
}