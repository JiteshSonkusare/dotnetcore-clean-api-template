using Microsoft.AspNetCore.Authorization;

namespace CCFCleanAPITemplate.Authentication;

public static class AuthorizationHandlerExtension
{
    public static RouteHandlerBuilder Authorize(this RouteHandlerBuilder endpoint, string? policy = null, string[]? roles = null, params string[] schemes)
    {
        var authorizeAttribute = new AuthorizeAttribute();

        if (policy != null && policy.Any())
        {
            authorizeAttribute.Policy = policy;
        }

        if (roles != null && roles.Any())
        {
            authorizeAttribute.Roles = string.Join(',', roles);
        }

        if (schemes != null && schemes.Any())
        {
            authorizeAttribute.AuthenticationSchemes = string.Join(',', schemes);
        }

        endpoint.WithMetadata(authorizeAttribute);

        return endpoint;
    }
}