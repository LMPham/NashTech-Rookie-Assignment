using System.Diagnostics.CodeAnalysis;

namespace Presentation.Infrastructure
{
    /// <summary>
    /// Extension of the <see cref="IEndpointRouteBuilder"/> interface
    /// for mapping API endpoints.
    /// </summary>
    public static class IEndpointRouteBuilderExtensions
    {
        /// <summary>
        /// Maps GET request endpoint.
        /// </summary>
        public static IEndpointRouteBuilder MapGet(this IEndpointRouteBuilder builder, Delegate handler, [StringSyntax("Route")] string pattern = "")
        {
            Guard.Against.AnonymousMethod(handler);

            builder.MapGet(pattern, handler)
                .WithName(handler.Method.Name);

            return builder;
        }

        /// <summary>
        /// Maps POST request endpoint.
        /// </summary>
        public static IEndpointRouteBuilder MapPost(this IEndpointRouteBuilder builder, Delegate handler, [StringSyntax("Route")] string pattern = "")
        {
            Guard.Against.AnonymousMethod(handler);

            builder.MapPost(pattern, handler)
                .WithName(handler.Method.Name);

            return builder;
        }

        /// <summary>
        /// Maps PUT request endpoint.
        /// </summary>
        public static IEndpointRouteBuilder MapPut(this IEndpointRouteBuilder builder, Delegate handler, [StringSyntax("Route")] string pattern="")
        {
            Guard.Against.AnonymousMethod(handler);

            builder.MapPut(pattern, handler)
                .WithName(handler.Method.Name);

            return builder;
        }

        /// <summary>
        /// Maps PATCH request endpoint.
        /// </summary>
        public static IEndpointRouteBuilder MapPatch(this IEndpointRouteBuilder builder, Delegate handler, [StringSyntax("Route")] string pattern = "")
        {
            Guard.Against.AnonymousMethod(handler);

            builder.MapPatch(pattern, handler)
                .WithName(handler.Method.Name);

            return builder;
        }

        /// <summary>
        /// Maps DELETE request endpoint.
        /// </summary>
        public static IEndpointRouteBuilder MapDelete(this IEndpointRouteBuilder builder, Delegate handler, [StringSyntax("Route")] string pattern="")
        {
            Guard.Against.AnonymousMethod(handler);

            builder.MapDelete(pattern, handler)
                .WithName(handler.Method.Name);

            return builder;
        }
    }
}
