using Application.UseCases.Departments.Commands.GetDepartment;
using AutoMapper;
using System.Diagnostics;

namespace Presentation.Controllers
{
    /// <summary>
    /// Controller for Home page.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly IUser user;
        private readonly IIdentityService identityService;

        public HomeController(
            IMediator _mediator,
            IMapper _mapper,
            IUser _user,
            IIdentityService _identityService)
        {
            mediator = _mediator;
            mapper = _mapper;
            user = _user;
            identityService = _identityService;
        }

        public async Task<IActionResult> Index([FromQuery] GetProductsWithPaginationCommand command)
        {
            var products = await mediator.Send(command);

            if (user.Id != null && IUser.Mode == null)
            {
                IUser.Mode = await identityService.GetUserModeAsync(user.Id);
            }

            var model = new HomeIndexModel
            {
                Products = products,
                Queries = mapper.Map<GetProductsWithPaginationCommand, LookupDto>(command),
                User = user,
            };

            if(products.Count == 0 && command.DepartmentId != null)
            {
                ViewData["selectedDepartment"] = await mediator.Send(new GetDepartmentCommand
                {
                    Id = (int) command.DepartmentId,
                });
            }

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Filter(LookupDto queries)
        {
            return RedirectToAction("Index", queries);
        }

        public IActionResult ClearFilterByPrice(LookupDto queries)
        {
            queries.ClearFilterByPrice();
            return RedirectToAction("Index", queries);
        }

        public IActionResult ClearFilterByCategoryId(LookupDto queries)
        {
            queries.ClearFilterByCategoryId();
            return RedirectToAction("Index", queries);
        }

        public IActionResult ClearFilterByCustomerReviewScore(LookupDto queries)
        {
            queries.ClearFilterByCustomerReviewScore();
            return RedirectToAction("Index", queries);
        }

        public IActionResult SwitchMode(string newMode)
        {
            if (user.Id != null)
            {
                IUser.Mode = newMode;
                identityService.UpdateUserModeAsync(user.Id, newMode);
            }
            return RedirectToAction("Index");
        }
    }
}
