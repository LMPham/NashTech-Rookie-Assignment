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

            product.Description = request.Description ?? product.Description;
            product.Price = request.Price ?? product.Price;

            product.AddDomainEvent(new ProductUpdatedEvent(product));

            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
