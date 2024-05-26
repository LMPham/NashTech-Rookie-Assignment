using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.Identity.JwtBearer
{
    /// <summary>
    /// Extensions for configuring custom JWT authentication
    /// </summary>
    public static class JwtBearerExtensions
    {
        /// <summary>
        /// Enables custom JWT-bearer authentication using the specified scheme.
        /// </summary>
        public static AuthenticationBuilder AddCustomJwtBearer(this AuthenticationBuilder builder)
            => builder.AddCustomJwtBearer(JwtBearerDefaults.AuthenticationScheme, _ => { });

        /// <summary>
        /// Enables custom JWT-bearer authentication using the specified scheme.
        /// </summary>
        public static AuthenticationBuilder AddCustomJwtBearer(this AuthenticationBuilder builder, 
            string authenticationScheme)
            => builder.AddCustomJwtBearer(authenticationScheme, _ => { });

        /// <summary>
        /// Enables custom JWT-bearer authentication using the specified scheme.
        /// </summary>
        public static AuthenticationBuilder AddCustomJwtBearer(this AuthenticationBuilder builder, 
            Action<CustomJwtBearerOptions> configureOptions) 
            => builder.AddCustomJwtBearer(JwtBearerDefaults.AuthenticationScheme, configureOptions);

        /// <summary>
        /// Enables custom JWT-bearer authentication using the specified scheme.
        /// </summary>
        public static AuthenticationBuilder AddCustomJwtBearer(this AuthenticationBuilder builder, 
            string authenticationScheme, Action<CustomJwtBearerOptions> configureOptions)
            => builder.AddCustomJwtBearer(authenticationScheme, displayName: null, configureOptions: configureOptions);

        /// <summary>
        /// Enables custom JWT-bearer authentication using the specified scheme.
        /// </summary>
        public static AuthenticationBuilder AddCustomJwtBearer(this AuthenticationBuilder builder, 
            string authenticationScheme, string? displayName, Action<CustomJwtBearerOptions> configureOptions)
        {
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IConfigureOptions<CustomJwtBearerOptions>, 
                CustomJwtBearerConfigureOptions>());
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IPostConfigureOptions<CustomJwtBearerOptions>, 
                JwtBearerPostConfigureOptions>());
            return builder.AddScheme<CustomJwtBearerOptions,
                CustomJwtBearerHandler>(authenticationScheme, displayName, configureOptions);
        }
    }
}
