
using Application.Common.Interfaces;
using Ardalis.GuardClauses;
using Domain.Events.Products;

namespace Application.UseCases.Products.Commands.CreateProduct
{
    /// <summary>
    /// Request to create a new Product.
    /// </summary>
    public record CreateProductCommand : IRequest<int>
    {
        public required string Name { get; init; }
        public required Category Category { get; init; }
        public string Description { get; init; } = String.Empty;
        public required int Price { get; init; }
        //public string? Image { get; init; }
    }

    /// <summary>
    /// Request handler for creating a new Product.
    /// </summary>
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IApplicationDbContext dbContext;

        public CreateProductCommandHandler(IApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        /// <summary>
        /// Creates a new Product and adds it into the database.
        /// The new Product cannot contain a new Category having non-null Id
        /// since the database does not allow identity inserting.
        /// </summary>
        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            // Checks if the Category of the Product exists in the database
            // If the Category exists, replaces it with the record in the database to prevent creating duplicate records
            var category = dbContext.Categories.Where(c => c.Id == request.Category.Id).FirstOrDefault();

            var product = new Product
            {
                Name = request.Name,
                Category = category ?? request.Category,
                Description = request.Description,
                Price = request.Price,
            };

            product.AddDomainEvent(new ProductCreatedEvent(product));

            dbContext.Products.Add(product);

            await dbContext.SaveChangesAsync(cancellationToken);

            return product.Id;
        }
    }
}
