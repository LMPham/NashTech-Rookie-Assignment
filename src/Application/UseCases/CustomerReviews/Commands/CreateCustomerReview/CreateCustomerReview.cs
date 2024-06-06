using Application.Common.Interfaces;
using Ardalis.GuardClauses;

namespace Application.UseCases.CustomerReviews.Commands.CreateCustomerReview
{
    /// <summary>
    /// Request to create a new CustomerReview.
    /// </summary>
    [Authorize]
    public class CreateCustomerReviewCommand : IRequest<int>
    {
        public int Score { get; init; } = 0;
        public string Headline { get; init; } = string.Empty;
        public string Comment { get; init; } = string.Empty;
        public required Product Product { get; init; } = new Product();
    }

    /// <summary>
    /// Request handler for creating a new CustomerReview.
    /// </summary>
    public class CreateCustomerReviewCommandHandler : IRequestHandler<CreateCustomerReviewCommand, int>
    {
        private readonly IApplicationDbContext dbContext;

        public CreateCustomerReviewCommandHandler(IApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<int> Handle(CreateCustomerReviewCommand request, CancellationToken cancellationToken)
        {
            // Check if the Product of the CustomerReview exists in the database.
            var product = dbContext.Products.Where(p => p.Id == request.Product.Id).FirstOrDefault();
            Guard.Against.NotFound(request.Product.Id, product);

            var customerReview = new CustomerReview
            {
                Score = request.Score,
                Headline = request.Headline,
                Comment = request.Comment,
                Product = product,
            };

            customerReview.AddDomainEvent(new CustomerReviewCreatedEvent(customerReview));

            product.CustomerReviews.Add(customerReview);

            await dbContext.SaveChangesAsync(cancellationToken);

            return customerReview.Id;
        }
    }
}
