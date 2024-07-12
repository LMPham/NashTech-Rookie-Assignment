namespace Presentation.Controllers;

/// <summary>
/// Controller for ProductDetails page.
/// </summary>
public class ProductDetailsController : Controller
{
    private readonly IMediator mediator;
    private readonly IUser user;
    private readonly IIdentityService identityService;

    public ProductDetailsController(IMediator _mediator, IUser _user, IIdentityService _identityService)
    {
        mediator = _mediator;
        user = _user;
        identityService = _identityService;
    }

    [HttpGet]
    [Route("[controller]/{id}")]
    public async Task<IActionResult> Index([FromRoute] int id)
    {
        if (user.Id != null && IUser.Mode == null)
        {
            IUser.Mode = await identityService.GetUserModeAsync(user.Id);
        }

        var product = await mediator.Send(new GetProductQuery { Id = id});

        var model = new ProductDetailsIndexModel
        {
            Product = product,
            User = user,
        };

        return View(model);
    }
}
