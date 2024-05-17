
using Application.Common.Interfaces;

namespace Application.UseCases.Products.Commands.CreateProduct
{
    public record CreateProductCommand : IRequest<int>
    {
        public required string Name { get; init; }
        public required Category Category { get; init; }
        public string Description { get; init; } = String.Empty;
        public required int Price { get; init; }
        //public string? Image { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IApplicationDbContext dbContext;

        public CreateProductCommandHandler(IApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                Category = request.Category,
                Description = request.Description,
                Price = request.Price,
            };

            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync(cancellationToken);

            //product.AddDomainEvent(...)

            return product.Id;
        }
    }
}
