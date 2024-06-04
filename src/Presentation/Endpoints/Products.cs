using Application.UseCases.Products.Commands.GetProduct;

namespace Presentation.Endpoints
{
    /// <summary>
    /// Product API endpoint group for handling Product-related services.
    /// </summary>
    public class Products : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            app.MapGroup(this)
                .MapGet(GetProductsWithPagination)
                .MapGet(GetProduct, "Detail/{id}")
                .MapPost(CreateProduct)
                .MapPatch(UpdateProduct, "{id}")
                .MapDelete(DeleteProduct, "{id}");
        }
        
        public Task<PaginatedList<ProductBriefDto>> GetProductsWithPagination(ISender sender, [AsParameters] GetProductsWithPaginationCommand command)
        {
            return sender.Send(command);
        }

        public Task<ProductBriefDto> GetProduct(ISender sender, int id)
        {
            var command = new GetProductCommand { Id = id };
            return sender.Send(command);
        }

        public Task<int> CreateProduct(ISender sender, CreateProductCommand command)
        {
            return sender.Send(command);
        }

        public async Task<IResult> UpdateProduct(ISender sender, int id, UpdateProductCommand command)
        {
            if(id != command.Id)
            {
                return Results.BadRequest();
            }
            await sender.Send(command);
            return Results.NoContent();
        }
       
        public async Task<IResult> DeleteProduct(ISender sender, int id)
        {
            await sender.Send(new DeleteProductCommand { Id = id});
            return Results.NoContent();
        }
    }
}
