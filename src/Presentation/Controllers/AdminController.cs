using Microsoft.EntityFrameworkCore;

namespace Presentation.Controllers;

/// <summary>
/// Controller for Admin page.
/// </summary>
public class AdminController : Controller
{
    private readonly IApplicationDbContext dbContext;
    private readonly IUser user;
    private readonly IIdentityService identityService;
    public AdminController(
        IApplicationDbContext _dbContext,
        IUser _user,
        IIdentityService _identityService)
    {
        dbContext = _dbContext;
        user = _user;
        identityService = _identityService;
    }
    [Authorize(Roles = Roles.Administrator)]
    public async Task<IActionResult> Index()
    {
        var departments = dbContext.Departments
            .Include(d => d.Products)
            .Include(d => d.Categories)
            .OrderBy(d => d.Name).ToList();
        var model = new AdminIndexModel
        {
            User = user,
            Departments = departments,
        };

        return View(model);
    }
}
