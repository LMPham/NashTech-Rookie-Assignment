
using Application.Common.Interfaces;
using Ardalis.GuardClauses;

namespace Application.UseCases.Products.Commands.CreateProduct
{
    public record CreateProductCommand : IRequest<int>
    {
        public required string Name { get; init; }
        public required Category Category { get; init; }
        public string Description { get; init; } = String.Empty;
        public required int Price { get; init; }
        //public string? Image { get; init; }
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
            var category = dbContext.Categories.Where(c => c.Id == request.Category.Id).FirstOrDefault();

            var product = new Product
            {
                Name = request.Name,
                Category = category ?? request.Category,
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
