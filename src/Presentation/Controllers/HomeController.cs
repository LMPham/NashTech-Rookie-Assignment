using AutoMapper;
using System.Diagnostics;

namespace Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public HomeController(IMediator _mediator, IMapper _mapper)
        {
            mediator = _mediator;
            mapper = _mapper;
        }

        public async Task<IActionResult> Index([FromQuery] GetProductsWithPaginationCommand command)
        {
            var products = await mediator.Send(command);

            var model = new HomeIndexModel
            {
                Products = products,
                Queries = mapper.Map<GetProductsWithPaginationCommand, LookupDto>(command),
            };

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
    }
}
