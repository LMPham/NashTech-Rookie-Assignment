using System.Reflection;

namespace Presentation.Infrastructure
{
    /// <summary>
    /// Extensions of the <see cref="IGuardClause"/> interface 
    /// and <see cref="MethodInfo"/> class for checking and 
    /// guarding against anonymous method
    /// </summary>
    public static class MethodInfoExtensions
    {
        public static bool IsAnonymous(this MethodInfo method)
        {
            var invalidChars = new[] { '<', '>' };
            return method.Name.Any(invalidChars.Contains);
        }

        public static void AnonymousMethod(this IGuardClause guardClause, Delegate input)
        {
            if (input.Method.IsAnonymous())
                throw new ArgumentException("The endpoint name must be specified when using anonymous handlers.");
        }
    }
}
