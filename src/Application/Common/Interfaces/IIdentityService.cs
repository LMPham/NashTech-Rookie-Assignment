using Application.Common.Models;

namespace Application.Common.Interfaces
{
    /// <summary>
    /// Base interface for identity services.
    /// </summary>
    public interface IIdentityService
    {
        Task<string?> GetUserNameAsync(string userId);
        Task<string?> GetUserModeAsync(string userId);
        Task UpdateUserModeAsync(string userId, string mode);

        Task<bool> IsInRoleAsync(string userId, string role);

        Task<bool> AuthorizeAsync(string userId, string policyName);

        Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

        Task<Result> DeleteUserAsync(string userId);
    }
}
