namespace Shared.ApiClientHanlder.Security;

public interface IAuthHandler
{
    Task<IAuthToken> GetAuthToken(CancellationToken cancellation);
}