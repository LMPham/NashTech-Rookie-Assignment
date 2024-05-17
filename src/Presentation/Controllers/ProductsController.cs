using Application.UseCases.Products.Commands.CreateProduct;
using Application.UseCases.Products.Commands.DeleteProduct;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        private readonly IMediator mediator;

        public ProductsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost(Name = "Swagger/CreateProduct")]
        public async Task<int> Post(CreateProductCommand command)
        {
            return await mediator.Send(command);
        }

        [HttpDelete(Name = "Swagger/DeleteProduct")]
        public async Task<IResult> Delete(DeleteProductCommand command)
        {
            await mediator.Send(command);
            return Results.NoContent();
        }
    }
}
