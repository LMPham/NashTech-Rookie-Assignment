using Microsoft.AspNetCore.Authentication;

namespace Infrastructure.Identity.Services
{
    /// <summary>
    /// Microsofts' Microsoft.AspNetCore.Authentication.JwtBearer.<see cref="AuthenticateResult"/>
    /// </summary>
    internal static class AuthenticateResults
    {
        internal static AuthenticateResult ValidatorNotFound = AuthenticateResult.Fail("No SecurityTokenValidator available for token.");
        internal static AuthenticateResult TokenHandlerUnableToValidate = AuthenticateResult.Fail("No TokenHandler was able to validate the token.");
    }
}
