using Application.Common.Interfaces;
using Ardalis.GuardClauses;

namespace Application.UseCases.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest
    {
        public required int Id { get; init; }
    }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IApplicationDbContext dbContext;

        public DeleteProductCommandHandler(IApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = dbContext.Products.Where(p => p.Id == request.Id).FirstOrDefault();

            Guard.Against.NotFound(request.Id, product);
            
            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync(cancellationToken);

            //product.AddDomainEvent(...)
        }
    }
}
