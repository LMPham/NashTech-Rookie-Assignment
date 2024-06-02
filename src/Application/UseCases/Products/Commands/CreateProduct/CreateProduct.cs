using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Ardalis.GuardClauses;

namespace Application.UseCases.Products.Commands.CreateProduct
{
    /// <summary>
    /// Request to create a new Product.
    /// </summary>
    public record CreateProductCommand : IRequest<int>
    {
        public required string Name { get; init; }
        public required Department Department { get; init; }
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
        /// The new Product must be in a existing Department and
        /// have an existing Category that is in the respective Department
        /// </summary>
        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            // Checks if the Category and Department of the Product exists in the database.
            var category = dbContext.Categories.Where(c => c.Id == request.Category.Id).FirstOrDefault();
            Guard.Against.NotFound(request.Category.Id, category);

            var department = dbContext.Departments.Where(d => d.Id == request.Department.Id).FirstOrDefault();
            Guard.Against.NotFound(request.Department.Id, department);

            // Checks if the Category is in the Department
            if (!department.Categories.Contains(category))
            {
                throw new BadRequestException();
            }

            var product = new Product
            {
                Name = request.Name,
                Department = department,
                Category = category,
                Description = request.Description,
                Price = request.Price,
            };

            product.AddDomainEvent(new ProductCreatedEvent(product));

            department.Products.Add(product);

            await dbContext.SaveChangesAsync(cancellationToken);

            return product.Id;
        }
    }
}
