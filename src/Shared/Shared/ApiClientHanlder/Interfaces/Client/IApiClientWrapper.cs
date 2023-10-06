namespace Shared.ApiClientHanlder;

public interface IApiClientWrapper
{
    Task<ResponseData> Send(RequestParameters requestParameters, CancellationToken cancellation);
}