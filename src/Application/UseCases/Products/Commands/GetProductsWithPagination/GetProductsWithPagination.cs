using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.Products.Commands.GetProductsWithPagination
{
    /// <summary>
    /// Request for getting products with pagination.
    /// </summary>
    public record GetProductsWithPaginationCommand : IRequest<PaginatedList<ProductBriefDto>>
    {
        public int? DepartmentId { get; init; }
        public int? CategoryId { get; init; }
        public int? MinPrice { get; init; }
        public int? MaxPrice { get; init; }
        public string? Search {  get; init; }
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 50;
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
                .Include(p => p.Category.Department)
                .Where(p => 
                    (request.DepartmentId == null || request.DepartmentId == p.Department.Id)
                    && (request.CategoryId == null || request.CategoryId == p.Category.Id)
                    && (request.MinPrice == null || p.Price >= request.MinPrice)
                    && (request.MaxPrice == null || p.Price <= request.MaxPrice)
                    && (request.Search == null || p.Name.Contains(request.Search) || p.Description.Contains(request.Search)))
                .OrderBy(p => p.Price)
                .ProjectTo<ProductBriefDto>(mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
