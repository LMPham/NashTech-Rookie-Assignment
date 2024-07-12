namespace Presentation.Controllers;

/// <summary>
/// Controller for managing Identity.
/// </summary>
[ApiController]
[Route("[controller]/[action]")]
public class IdentityController : Controller
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;


    public IdentityController(
        IIdentityService _identityService,
        UserManager<ApplicationUser> _userManager, 
        RoleManager<IdentityRole> _roleManager)
    {
        userManager = _userManager;
        roleManager = _roleManager;
    }

    [HttpPost(Name = "Swagger/CreateRole")]
    public async Task<IdentityResult> CreateRole(string roleName)
    {
        // Check if the role already exists
        if (await roleManager.RoleExistsAsync(roleName))
        {
            // Role already exists, throws an exception
            throw new Exception();
        }

        var role = new IdentityRole(roleName);

        return await roleManager.CreateAsync(role);
    }

    [HttpPost(Name = "Swagger/AddUserToRole")]
    public async Task<IdentityResult> AddUserToRole(string userId, string roleName)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user != null)
        {
            // Add the user to the specified role
            return await userManager.AddToRoleAsync(user, roleName);
        }
        else
        {
            // If the user does not exist, throws an exception
            throw new Exception();
        }
    }
}
