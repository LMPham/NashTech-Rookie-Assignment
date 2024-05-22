using Application.Common.Interfaces;
using Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory;
        private readonly IAuthorizationService authorizationService;

        public IdentityService(
            UserManager<ApplicationUser> _userManager,
            IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory,
            IAuthorizationService _authorizationService)
        {
            userManager = _userManager;
            userClaimsPrincipalFactory = _userClaimsPrincipalFactory;
            authorizationService = _authorizationService;
        }

        /// <summary>
        /// Finds and returns a user, if any, who has the specified
        /// <paramref name="userId"/>.
        /// </summary>
        public async Task<string?> GetUserNameAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            return user?.UserName;
        }

        /// <summary>
        /// Creates an  <see cref="ApplicationUser"/> with the specified
        /// <paramref name="userName"/> and <paramref name="password"/>,
        /// as an asynchronous operation.
        /// </summary>
        public async Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password)
        {
            var user = new ApplicationUser
            {
                UserName = userName,
                Email = userName,
            };

            var result = await userManager.CreateAsync(user, password);

            return (result.ToApplicationResult(), user.Id);
        }

        /// <summary>
        /// Checks if the user with the specified <paramref name="userId"/> 
        /// is a member of the specified named role.
        /// </summary>
        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var user = await userManager.FindByIdAsync(userId);

            return user != null && await userManager.IsInRoleAsync(user, role);
        }

        /// <summary>
        /// Checks if the user with the specified <paramref name="userId"/>
        /// meets the specified <paramref name="policyName"/>.
        /// </summary>
        public async Task<bool> AuthorizeAsync(string userId, string policyName)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return false;
            }

            var principal = await userClaimsPrincipalFactory.CreateAsync(user);

            var result = await authorizationService.AuthorizeAsync(principal, policyName);

            return result.Succeeded;
        }

        /// <summary>
        /// Deletes the user with the specified <paramref name="userId"/>.
        /// </summary>
        public async Task<Result> DeleteUserAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            return user != null ? await DeleteUserAsync(user) : Result.Success();
        }

        /// <summary>
        /// Deletes the specified <paramref name="user"/>.
        /// </summary>
        public async Task<Result> DeleteUserAsync(ApplicationUser user)
        {
            var result = await userManager.DeleteAsync(user);

            return result.ToApplicationResult();
        }
    }
}
