using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using System.Diagnostics;

namespace Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IMediator mediator;

        public HomeController(ILogger<HomeController> _logger, IMediator _mediator)
        {
            logger = _logger;
            mediator = _mediator;
        }

        public async Task<IActionResult> Index()
        {
            GetProductsWithPaginationCommand command = new GetProductsWithPaginationCommand
            {
                CategoryId = 10,
                PageNumber = 1,
                PageSize = 2,
            };

            var products = await mediator.Send(command);

            return View(products);

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
    }
}
