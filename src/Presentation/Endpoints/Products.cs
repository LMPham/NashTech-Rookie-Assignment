using Application.UseCases.Products.Commands.CreateProduct;
using Application.UseCases.Products.Commands.DeleteProduct;

namespace Presentation.Endpoints
{
    public class Products : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            app.MapGroup(this)
                .MapPost(CreateProduct)
                .MapDelete(DeleteProduct, "{id}");
        }
        
        public Task<int> CreateProduct(ISender sender, CreateProductCommand command)
        {
            return sender.Send(command);
        }
       
        public async Task<IResult> DeleteProduct(ISender sender, int id)
        {
            await sender.Send(new DeleteProductCommand { Id = id});
            return Results.NoContent();
        }
    }
}
