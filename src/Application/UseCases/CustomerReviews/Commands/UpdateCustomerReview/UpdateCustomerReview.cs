using Application.Common.Interfaces;
using Ardalis.GuardClauses;
using Domain.Entities;

namespace Application.UseCases.CustomerReviews.Commands.UpdateCustomerReview
{
    /// <summary>
    /// Request to update an existing CustomerReview.
    /// </summary>
    [Authorize]
    public class UpdateCustomerReviewCommand : IRequest
    {
        public required int Id { get; init; }
        public int? Score { get; init; }
        public string? Headline { get; init; }
        public string? Comment { get; init; }
    }

    public class UpdateCustomerReviewCommandHandler : IRequestHandler<UpdateCustomerReviewCommand>
    {
        private readonly IApplicationDbContext dbContext;

        public UpdateCustomerReviewCommandHandler(IApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task Handle(UpdateCustomerReviewCommand request, CancellationToken cancellationToken)
        {
            var customerReview = dbContext.CustomerReviews.Where(cr =>  cr.Id == request.Id).FirstOrDefault();
            
            // Checks if the CustomerReview exist. If not, throws an exception
            Guard.Against.NotFound(request.Id, customerReview);

            customerReview.Score = request.Score ?? customerReview.Score;
            customerReview.Headline = request.Headline ?? customerReview.Headline;
            customerReview.Comment = request.Comment ?? customerReview.Comment;

            customerReview.AddDomainEvent(new CustomerReviewUpdatedEvent(customerReview));

            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
