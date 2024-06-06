using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;

namespace Application.UseCases.CustomerReviews.Commands.GetCustomerReviewsWithPagination
{
    /// <summary>
    /// Request for getting CustomerReviews with pagination.
    /// </summary>
    public class GetCustomerReviewsWithPaginationCommand : IRequest<PaginatedList<CustomerReview>>
    {
        public int? ProductId { get; init; }
        public string? UserId { get; init; }
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 50;
    }

    /// <summary>
    /// Request handler for getting CustomerReviews with pagination.
    /// </summary>
    public class GetCustomerReviewsWithPaginationCommandHandler : IRequestHandler<GetCustomerReviewsWithPaginationCommand, PaginatedList<CustomerReview>>
    {
        private readonly IApplicationDbContext dbContext;

        public GetCustomerReviewsWithPaginationCommandHandler(IApplicationDbContext _context)
        {
            dbContext = _context;
        }

        public async Task<PaginatedList<CustomerReview>> Handle(GetCustomerReviewsWithPaginationCommand request, CancellationToken cancellationToken)
        {
            return await dbContext.CustomerReviews
                .Where(cr =>
                    (request.ProductId == null || request.ProductId == cr.Product.Id)
                    && (request.UserId == null || request.UserId == cr.CreatedBy))
                .OrderBy(cr => cr.Created)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
