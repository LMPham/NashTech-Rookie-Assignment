using Application.Common.Interfaces;
using Ardalis.GuardClauses;
using AutoMapper;

namespace Application.UseCases.CustomerReviews.Commands.GetCustomerReview
{
    /// <summary>
    /// Request for getting an existing CustomerReview.
    /// </summary>
    public class GetCustomerReviewCommand : IRequest<CustomerReview>
    {
        public required int Id { get; init; }
    }

    public class GetCustomerReviewCommandHandler : IRequestHandler<GetCustomerReviewCommand, CustomerReview>
    {
        private readonly IApplicationDbContext dbContext;

        public GetCustomerReviewCommandHandler(IApplicationDbContext _context)
        {
            dbContext = _context;
        }

        public async Task<CustomerReview> Handle(GetCustomerReviewCommand request, CancellationToken cancellationToken)
        {
            var customerReview = dbContext.CustomerReviews.Where(cr => cr.Id == request.Id).FirstOrDefault();

            // Checks if the CustomerReview exist. If not, throws an exception
            Guard.Against.NotFound(request.Id, customerReview);

            return customerReview;
        }
    }
}
