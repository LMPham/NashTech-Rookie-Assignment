using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Application.UseCases.Products.Commands.GetProductsWithPagination
{
    /// <summary>
    /// Request for getting products with pagination.
    /// </summary>
    public record GetProductsWithPaginationCommand : IRequest<PaginatedList<ProductBriefDto>>
    {
        public int CategoryId { get; init; }
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    /// <summary>
    /// Request handler for getting products with pagination.
    /// </summary>
    public class GetProductsWithPaginationCommandHandler : IRequestHandler<GetProductsWithPaginationCommand, PaginatedList<ProductBriefDto>>
    {
        private readonly IApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public GetProductsWithPaginationCommandHandler(IApplicationDbContext _context, IMapper _mapper)
        {
            dbContext = _context;
            mapper = _mapper;
        }

        public async Task<PaginatedList<ProductBriefDto>> Handle(GetProductsWithPaginationCommand request, CancellationToken cancellationToken)
        {
            return await dbContext.Products
                .Where(product => product.Category.Id == request.CategoryId)
                .OrderBy(product => product.Price)
                .ProjectTo<ProductBriefDto>(mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
