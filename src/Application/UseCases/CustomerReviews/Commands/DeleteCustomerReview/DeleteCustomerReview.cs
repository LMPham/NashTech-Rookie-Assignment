using Application.Common.Interfaces;
using Ardalis.GuardClauses;

namespace Application.UseCases.CustomerReviews.Commands.DeleteCustomerReview;

/// <summary>
/// Request to delete an existing CustomerReview.
/// </summary>
[Authorize]
public class DeleteCustomerReviewCommand : IRequest
{
    public required int Id { get; init; }
}

/// <summary>
/// Request handler for deleting an existing CustomerReview.
/// </summary>
public class DeleteCustomerReviewCommandHandler : IRequestHandler<DeleteCustomerReviewCommand>
{
    private readonly IApplicationDbContext dbContext;

    public DeleteCustomerReviewCommandHandler(IApplicationDbContext _dbContext)
    {
        dbContext = _dbContext;
    }

    public async Task Handle(DeleteCustomerReviewCommand request, CancellationToken cancellationToken)
    {
        var customerReview = dbContext.CustomerReviews.Where(cr => cr.Id == request.Id).FirstOrDefault();

        // Checks if the CustomerReview exist. If not, throws an exception
        Guard.Against.NotFound(request.Id, customerReview);

        dbContext.CustomerReviews.Remove(customerReview);

        customerReview.AddDomainEvent(new CustomerReviewDeletedEvent(customerReview));

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
