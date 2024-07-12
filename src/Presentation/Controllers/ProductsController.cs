namespace Presentation.Controllers;

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
    public async Task<PaginatedList<ProductDto>> Get([FromQuery] GetProductsWithPaginationQuery query)
    {
        return await mediator.Send(query);
    }

    [HttpGet(Name = "Swagger/GetProductDetail")]
    [ActionName("GetDetail")]
    public async Task<ProductDto> GetDetail([FromQuery] GetProductQuery query)
    {
        return await mediator.Send(query);
    }

    [HttpPost(Name = "Swagger/CreateProduct")]
    [Authorize(Roles = Roles.Administrator)]
    public async Task<int> Post(CreateProductCommand command)
    {
        return await mediator.Send(command);
    }

    [HttpPatch(Name = "Swagger/UpdateProduct")]
    [Authorize(Roles = Roles.Administrator)]
    public async Task<IResult> Patch(UpdateProductCommand command)
    {
        await mediator.Send(command);
        return Results.NoContent();
    }

    [HttpDelete(Name = "Swagger/DeleteProduct")]
    [Authorize(Roles = Roles.Administrator)]
    public async Task<IResult> Delete(DeleteProductCommand command)
    {
        await mediator.Send(command);
        return Results.NoContent();
    }
}
