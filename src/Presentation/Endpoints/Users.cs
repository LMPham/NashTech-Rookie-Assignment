using System.Security.Claims;

namespace Presentation.Endpoints;

/// <summary>
/// User API endpoint group for handling User-related services.
/// </summary>
public class Users : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetMe, "/me")
            .MapIdentityApi<ApplicationUser>();
    }

    public async Task<AuthUser> GetMe(UserManager<ApplicationUser> userManager, ClaimsPrincipal user)
    {
        var applicationUser = await userManager.GetUserAsync(user);

        Guard.Against.Null(applicationUser);

        var claims = await userManager.GetClaimsAsync(applicationUser);

        return new AuthUser
        {
            Id = applicationUser.Id,
            Claims = claims
        };
    }

    public sealed class AuthUser
    {
        public string Id { get; set; } = string.Empty;
        public IEnumerable<Claim> Claims { get; set; } = [];
    }
}