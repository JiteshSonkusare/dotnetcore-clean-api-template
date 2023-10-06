using Shared.ApiClientHanlder.Security;

namespace Shared.ApiClientHanlder;

public record RequestParameters
{
    public Uri RequestUri { get; set; } = null!;
    public HttpMethod Method { get; set; } = null!;
    public HttpContent? RequestContent { get; set; }
    public IAuthHandler? AuthHandler { get; set; }
    public HeaderData[]? RequestHeaders { get; set; }
    public DefaultRequestHeaders? DefaultRequestHeaders { get; set; }
}