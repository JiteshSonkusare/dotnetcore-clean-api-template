using System.Net.Http.Headers;

namespace Shared.ApiClientHanlder.Security;

public static class AuthExtensions
{
    public static AuthenticationHeaderValue GetAuthorizationHeader(this IAuthToken token)
        => new(token.Scheme, token.Value);
}