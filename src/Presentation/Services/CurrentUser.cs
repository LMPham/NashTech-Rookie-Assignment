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
    }
}
