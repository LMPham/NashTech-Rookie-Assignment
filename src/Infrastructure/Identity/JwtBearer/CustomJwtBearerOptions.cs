using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;

namespace Infrastructure.Identity.JwtBearer
{
    /// <summary>
    /// Custom derived class of Microsoft's Microsoft.AspNetCore.Authentication.JwtBearer
    /// <see cref="JwtBearerOptions"/> that includes token expiration time.
    /// </summary>
    public class CustomJwtBearerOptions : JwtBearerOptions
    {
        private ISecureDataFormat<AuthenticationTicket>? bearerTokenProtector;
        private ISecureDataFormat<AuthenticationTicket>? refreshTokenProtector;

        /// <summary>
        /// Controls how much time the bearer token will remain valid from the point it is created.
        /// The expiration information is stored in the protected token. Because of that, an expired token will be rejected
        /// even if it is passed to the server after the client should have purged it.
        /// </summary>
        /// <remarks>
        /// Defaults to 1 hour.
        /// </remarks>
        public TimeSpan BearerTokenExpiration { get; set; } = TimeSpan.FromHours(1);

        /// <summary>
        /// Controls how much time the refresh token will remain valid from the point it is created.
        /// The expiration information is stored in the protected token.
        /// </summary>
        /// <remarks>
        /// Defaults to 14 days.
        /// </remarks>
        public TimeSpan RefreshTokenExpiration { get; set; } = TimeSpan.FromDays(14);

        /// <summary>
        /// If set, the <see cref="BearerTokenProtector"/> is used to protect and unprotect the identity and other properties which are stored in the
        /// bearer token. If not provided, one will be created using <see cref="TicketDataFormat"/> and the <see cref="IDataProtectionProvider"/>
        /// from the application <see cref="IServiceProvider"/>.
        /// </summary>
        public ISecureDataFormat<AuthenticationTicket> BearerTokenProtector
        {
            get => bearerTokenProtector ?? throw new InvalidOperationException($"{nameof(BearerTokenProtector)} was not set.");
            set => bearerTokenProtector = value;
        }

        /// <summary>
        /// If set, the <see cref="RefreshTokenProtector"/> is used to protect and unprotect the identity and other properties which are stored in the
        /// refresh token. If not provided, one will be created using <see cref="TicketDataFormat"/> and the <see cref="IDataProtectionProvider"/>
        /// from the application <see cref="IServiceProvider"/>.
        /// </summary>
        public ISecureDataFormat<AuthenticationTicket> RefreshTokenProtector
        {
            get => refreshTokenProtector ?? throw new InvalidOperationException($"{nameof(RefreshTokenProtector)} was not set.");
            set => refreshTokenProtector = value;
        }
    }
}
