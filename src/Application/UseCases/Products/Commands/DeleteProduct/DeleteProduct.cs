using Application.Common.Interfaces;
using Ardalis.GuardClauses;

namespace Application.UseCases.Products.Commands.DeleteProduct
{
    /// <summary>
    /// Request to delete an existing Product.
    /// </summary>
    [Authorize(Roles = Roles.Administrator)]
    public class DeleteProductCommand : IRequest
    {
        public required int Id { get; init; }
    }

    /// <summary>
    /// Request handler for deleting an existing Product.
    /// </summary>
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IApplicationDbContext dbContext;

        public DeleteProductCommandHandler(IApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        /// <summary>
        /// Finds the corresponding Product and removes it from the database.
        /// </summary>
        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = dbContext.Products.Where(p => p.Id == request.Id).FirstOrDefault();
            
            // Checks if the Product exists. If not, throws an exception
            Guard.Against.NotFound(request.Id, product);

            List<ProductDetail> productDetails = dbContext.ProductDetails.Where(pd => pd.ProductId == product.Id).ToList();
            dbContext.ProductDetails.RemoveRange(productDetails);

            List<Image> productImages = dbContext.Images.Where(i => i.ProductId == product.Id).ToList();
            dbContext.Images.RemoveRange(productImages);

            dbContext.Products.Remove(product);

            product.AddDomainEvent(new ProductDeletedEvent(product));

            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
