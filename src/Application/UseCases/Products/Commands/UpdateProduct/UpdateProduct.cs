using Application.Common.Interfaces;
using Ardalis.GuardClauses;

namespace Application.UseCases.Products.Commands.UpdateProduct
{
    /// <summary>
    /// Request to update an existing Product.
    /// </summary>
    [Authorize(Roles = Roles.Administrator)]
    public class UpdateProductCommand : IRequest
    {
        public required int Id { get; init; }
        public string? Name { get; init; }
        public List<string>? Descriptions { get; init; }
        public List<ProductDetail>? Details { get; set; }
        public List<CustomerReview>? CustomerReviews { get; set; }
        public int? Quantity { get; set; }
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
            product.Descriptions = request.Descriptions ?? product.Descriptions;
            product.Details = request.Details ?? product.Details;
            product.CustomerReviews = request.CustomerReviews ?? product.CustomerReviews;
            product.Quantity = request.Quantity ?? product.Quantity;
            product.Price = request.Price ?? product.Price;

            product.AddDomainEvent(new ProductUpdatedEvent(product));

            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
