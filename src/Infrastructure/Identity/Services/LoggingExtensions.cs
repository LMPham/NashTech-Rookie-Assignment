using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Services
{
    /// <summary>
    /// Microsofts' Microsoft.Extensions.Logging.<see cref="LoggingExtensions"/>
    /// </summary>
    internal static partial class LoggingExtensions
    {
        [LoggerMessage(1, LogLevel.Information, "Failed to validate the token.", EventName = "TokenValidationFailed")]
        public static partial void TokenValidationFailed(this ILogger logger, Exception ex);

        [LoggerMessage(2, LogLevel.Debug, "Successfully validated the token.", EventName = "TokenValidationSucceeded")]
        public static partial void TokenValidationSucceeded(this ILogger logger);

        [LoggerMessage(3, LogLevel.Error, "Exception occurred while processing message.", EventName = "ProcessingMessageFailed")]
        public static partial void ErrorProcessingMessage(this ILogger logger, Exception ex);

        [LoggerMessage(4, LogLevel.Debug, "Unable to reject the response as forbidden, it has already started.", EventName = "ForbiddenResponseHasStarted")]
        public static partial void ForbiddenResponseHasStarted(this ILogger logger);

        [LoggerMessage(1, LogLevel.Information, "AuthenticationScheme: {AuthenticationScheme} signed in.", EventName = "AuthenticationSchemeSignedIn")]
        public static partial void AuthenticationSchemeSignedIn(this ILogger logger, string authenticationScheme);
    }
}
