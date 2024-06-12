using Application.Common.Interfaces;
using Application.Common.Models;
using Ardalis.GuardClauses;
using AutoMapper;

namespace Application.UseCases.Products.Queries.GetProduct
{
    /// <summary>
    /// Request for getting an existing Product.
    /// </summary>
    public class GetProductQuery : IRequest<ProductDto>
    {
        public required int Id { get; init; }
    }

    /// <summary>
    /// Request handler for getting an existing Product.
    /// </summary>
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductDto>
    {
        private readonly IApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public GetProductQueryHandler(IApplicationDbContext _context, IMapper _mapper)
        {
            dbContext = _context;
            mapper = _mapper;
        }

        public async Task<ProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = dbContext.Products
                .Include(p => p.CustomerReviews)
                .Include(p => p.Details)
                .Include(p => p.Images)
                .Where(p => p.Id == request.Id)
                .FirstOrDefault();

            // Checks if the Product exists. If not, throws an exception
            Guard.Against.NotFound(request.Id, product);

            return mapper.Map<Product, ProductDto>(product);
        }
    }
}
