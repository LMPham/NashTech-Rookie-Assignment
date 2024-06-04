namespace Presentation.Controllers
{
    /// <summary>
    /// Controller for managing Products.
    /// </summary>
    [ApiController]
    [Route("[controller]/[action]")]
    public class ProductsController : Controller
    {
        private readonly IMediator mediator;

        public ProductsController(IMediator _mediator)
        {
            mediator = _mediator;
        }

        [HttpGet(Name = "Swagger/GetProducts")]
        public async Task<PaginatedList<ProductBriefDto>> Get([FromQuery] GetProductsWithPaginationCommand command)
        {
            return await mediator.Send(command);
        }

        [HttpGet(Name = "Swagger/GetProductDetail")]
        [ActionName("GetDetail")]
        public async Task<ProductBriefDto> GetDetail([FromQuery] GetProductCommand command)
        {
            return await mediator.Send(command);
        }

        [HttpPost(Name = "Swagger/CreateProduct")]
        public async Task<int> Post(CreateProductCommand command)
        {
            return await mediator.Send(command);
        }

        [HttpPatch(Name = "Swagger/UpdateProduct")]
        public async Task<IResult> Patch(UpdateProductCommand command)
        {
            await mediator.Send(command);
            return Results.NoContent();
        }

        [HttpDelete(Name = "Swagger/DeleteProduct")]
        public async Task<IResult> Delete(DeleteProductCommand command)
        {
            await mediator.Send(command);
            return Results.NoContent();
        }
    }
}
