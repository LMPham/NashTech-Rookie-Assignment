using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Application.UseCases.Products.Queries.GetProductsWithPagination;

/// <summary>
/// Request for getting products with pagination.
/// </summary>
public record GetProductsWithPaginationQuery : IRequest<PaginatedList<ProductDto>>
{
    public int? DepartmentId { get; init; }
    public int? CategoryId { get; init; }
    public int? MinPrice { get; init; }
    public int? MaxPrice { get; init; }
    public string? Search { get; init; }
    public int? MinCustomerReviewScore { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 50;
}

/// <summary>
/// Request handler for getting products with pagination.
/// </summary>
public class GetProductsWithPaginationQueryHandler : IRequestHandler<GetProductsWithPaginationQuery, PaginatedList<ProductDto>>
{
    private readonly IApplicationDbContext dbContext;
    private readonly IMapper mapper;

    public GetProductsWithPaginationQueryHandler(IApplicationDbContext _context, IMapper _mapper)
    {
        dbContext = _context;
        mapper = _mapper;
    }

    public async Task<PaginatedList<ProductDto>> Handle(GetProductsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await dbContext.Products
            .Include(p => p.CustomerReviews)
            .Include(p => p.Category.Department)
            .Where(p =>
                (request.DepartmentId == null || request.DepartmentId == p.Department.Id)
                && (request.CategoryId == null || request.CategoryId == p.Category.Id)
                && (request.MinPrice == null || p.Price >= request.MinPrice)
                && (request.MaxPrice == null || p.Price <= request.MaxPrice)
                && (request.Search == null || p.Name.Contains(request.Search)
                    || p.Descriptions.Any(d => d.Contains(request.Search))
                    || p.Details.Any(d => d.Description.Contains(request.Search)))
                && (request.MinCustomerReviewScore == null
                    || !p.CustomerReviews.Any() && request.MinCustomerReviewScore == 0
                    || p.CustomerReviews.Any() && p.CustomerReviews.Average(cr => cr.Score) >= request.MinCustomerReviewScore))
            .OrderBy(p => p.Price)
            .ProjectTo<ProductDto>(mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
