using Application.Common.Interfaces;
using Ardalis.GuardClauses;

namespace Application.UseCases.Products.Commands.UpdateProduct
{
    /// <summary>
    /// Request to update an existing Product.
    /// </summary>
    public class UpdateProductCommand : IRequest
    {
        public required int Id { get; init; }
        public string? Name { get; init; }
        public Category? Category { get; init; }
        public string? Description { get; init; }
        public int? Price { get; init; }
        //public string? Image { get; init; }
    }

    /// <summary>
    /// Request handler for updating an existing Product.
    /// </summary>
    public class  UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IApplicationDbContext dbContext;

        public UpdateProductCommandHandler(IApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        /// <summary>
        /// Finds the corresponding Product and updates it.
        /// Throws an exception if the Product does not exist.
        /// </summary>
        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = dbContext.Products.Where(p => p.Id == request.Id).FirstOrDefault();

            // Checks if the Product exist. If not, throws an exception
            Guard.Against.NotFound(request.Id, product);

            product.Name = request.Name ?? product.Name;
            // Checks if the Category of the Product exists. If not, throws an exception.
            // Otherwise, replaces it with the record in the database to prevent creating a new Category record
            if (request.Category != null)
            {
                var category = dbContext.Categories.Where(c => c.Id == request.Category.Id).FirstOrDefault();

                Guard.Against.NotFound(request.Category.Id, category);
                product.Category = category;
            }
            product.Description = request.Description ?? product.Description;
            product.Price = request.Price ?? product.Price;
            await dbContext.SaveChangesAsync(cancellationToken);

            //product.AddDomainEvent(...)
        }
    }
}
