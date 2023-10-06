using NLog;
using Shared.ApiClientHanlder.Security;

namespace Shared.ApiClientHanlder;

public class ApiClientWrapper : IApiClientWrapper
{
    private readonly IHttpClientFactory _httpClientFactory;
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
    
    public ApiClientWrapper(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<ResponseData> Send(RequestParameters requestParameters, CancellationToken cancellation)
    {
        var client = _httpClientFactory.CreateClient();
        if (requestParameters.DefaultRequestHeaders != null)
            AssignDefaultRequestHeaders(client, requestParameters.DefaultRequestHeaders);

        if (requestParameters.AuthHandler != null)
        {
            var token = await requestParameters.AuthHandler.GetAuthToken(cancellation).ConfigureAwait(false);
            client.DefaultRequestHeaders.Authorization = token.GetAuthorizationHeader();
        }

        using var requestMessage = new HttpRequestMessage(requestParameters.Method, requestParameters.RequestUri);

        _logger.Debug($"Executing HTTP request. TargetUri:{requestParameters.RequestUri}, Method:{requestParameters.Method}.");

        if (requestParameters.RequestContent != null)
            requestMessage.Content = requestParameters.RequestContent;

        if (requestParameters.RequestHeaders != null)
            foreach (var header in requestParameters.RequestHeaders)
                requestMessage.Headers.Add(header.Name, header.Values);

        using HttpResponseMessage responseMessage = await client.SendAsync(requestMessage, cancellation).ConfigureAwait(false);

        if (!responseMessage.IsSuccessStatusCode)
            _logger.Warn($"Unsuccessful status ({responseMessage.StatusCode}) received.");

        string responseContent = await responseMessage.Content.ReadAsStringAsync(cancellation).ConfigureAwait(false);

        _logger.Trace($"HTTP Response StatusCode: {responseMessage.StatusCode}");

        return new ResponseData(responseMessage.StatusCode, responseContent, responseMessage.Headers, responseMessage.TrailingHeaders);
    }

    private static void AssignDefaultRequestHeaders(HttpClient client, DefaultRequestHeaders defaultRequestHeaders)
    {
        if (defaultRequestHeaders?.AcceptedMediaTypes?.Any() ?? false)
        {
            foreach (string mediaType in defaultRequestHeaders.AcceptedMediaTypes)
                client.DefaultRequestHeaders.Accept.ParseAdd(mediaType);
        }
        if (defaultRequestHeaders?.Headers?.Any() ?? false)
        {
            foreach (HeaderData header in defaultRequestHeaders.Headers)
                client.DefaultRequestHeaders.Add(header.Name, header.Values);
        }
    }
}