using Application.Common.Interfaces;
using System.Security.Claims;

namespace Presentation.Services
{
    /// <summary>
    /// Current user.
    /// </summary>
    public class CurrentUser : IUser
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public CurrentUser(IHttpContextAccessor _httpContextAccessor)
        {
            httpContextAccessor = _httpContextAccessor;
        }

        public string? Id => httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        public string? UserName => httpContextAccessor.HttpContext?.User?.Identity?.Name;

        public bool IsInRole(string roleName)
        {
            var httpContext = httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                var user = httpContext.User;
                if(user != null && user.Identity != null && user.Identity.IsAuthenticated)
                {
                    return user.IsInRole(roleName);
                }
            }
            return false;
        }
    }
}
