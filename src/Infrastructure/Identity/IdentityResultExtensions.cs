using Application.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    /// <summary>
    /// Extensions of the <see cref="IdentityResult"/> class
    /// for converting <see cref="IdentityResult"/> to the application's
    /// <see cref="Result"/>.
    /// </summary>
    public static class IdentityResultExtensions
    {
        public static Result ToApplicationResult(this IdentityResult result)
        {
            return result.Succeeded
                ? Result.Success()
                : Result.Failure(result.Errors.Select(e => e.Description));
        }
    }
}
