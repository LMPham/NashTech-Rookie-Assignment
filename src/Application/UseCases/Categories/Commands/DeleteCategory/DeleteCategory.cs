using Application.Common.Interfaces;
using Ardalis.GuardClauses;

namespace Application.UseCases.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest
    {
        public required int Id { get; init; }
    }

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly IApplicationDbContext dbContext;

        public DeleteCategoryCommandHandler(IApplicationDbContext _dbContext)
        {
           dbContext = _dbContext;
        }

        public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = dbContext.Categories.Where(c => c.Id == request.Id).FirstOrDefault();

            Guard.Against.NotFound(request.Id, category);

            dbContext.Categories.Remove(category);
            await dbContext.SaveChangesAsync(cancellationToken);

            //category.AddDomainEvent(...)
        }
    }
}
