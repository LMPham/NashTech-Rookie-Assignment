using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    /// <summary>
    /// The application's user.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        // The mode that the user is using.
        // E.g Admin or Customer mode.
        public string? Mode { get; set; }
    }
}
