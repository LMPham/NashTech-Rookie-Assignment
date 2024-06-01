using Application.Common.Models;
using Application.UseCases.Products.Commands.CreateProduct;
using Application.UseCases.Products.Commands.DeleteProduct;
using Application.UseCases.Products.Commands.GetProductsWithPagination;
using Application.UseCases.Products.Commands.UpdateProduct;
using Microsoft.AspNetCore.Mvc;

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
                .MapPost(CreateProduct)
                .MapPatch(UpdateProduct, "{id}")
                .MapDelete(DeleteProduct, "{id}");
        }
        
        public Task<PaginatedList<ProductBriefDto>> GetProductsWithPagination(ISender sender, [AsParameters] GetProductsWithPaginationCommand command)
        {
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
