using Application.UseCases.Products.Commands.CreateProduct;
using Presentation.Infrastructure;

namespace Presentation.Endpoints
{
    public class Products : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            //app.MapGroup(this)
            //    .MapPost(CreateProduct);
            app.MapGroup(this)
                .MapPost(CreateProduct);
        }
        
        public Task<int> CreateProduct(ISender sender, CreateProductCommand command)
        {
            return sender.Send(command);
        }

        public string Bello()
        {
            return "Bello";
        }
    }
}
