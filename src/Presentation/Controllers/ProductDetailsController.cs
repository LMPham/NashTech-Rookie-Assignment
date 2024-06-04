namespace Presentation.Controllers
{
    public class ProductDetailsController : Controller
    {
        private readonly IMediator mediator;

        public ProductDetailsController(IMediator _mediator)
        {
            mediator = _mediator;
        }

        [Route("[controller]/{id}")]
        public async Task<IActionResult> Index([FromRoute] int id)
        {
            var product = await mediator.Send(new GetProductCommand { Id = id});

            var model = new ProductDetailsIndexModel
            {
                Product = product,
            };

            return View(model);
        }

        public IActionResult Display([FromQuery] ProductBriefDto product)
        {
            var model = new ProductDetailsIndexModel
            {
                Product = product,
            };

            return View("Index", model);
        }
    }
}
