using Application.Common.Interfaces;
using Ardalis.GuardClauses;

namespace Application.UseCases.Categories.Commands.DeleteCategory
{
    /// <summary>
    /// Request to delete an existing Category.
    /// </summary>
    public class DeleteCategoryCommand : IRequest
    {
        public required int Id { get; init; }
    }

    /// <summary>
    /// Request handler for deleting an existing Category.
    /// </summary>
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly IApplicationDbContext dbContext;

        public DeleteCategoryCommandHandler(IApplicationDbContext _dbContext)
        {
           dbContext = _dbContext;
        }

        /// <summary>
        /// Finds the corresponding Category and removes it from the database.
        /// </summary>
        public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = dbContext.Categories.Where(c => c.Id == request.Id).FirstOrDefault();

            // Checks if the Category exists. If not, throws an exception
            Guard.Against.NotFound(request.Id, category);

            dbContext.Categories.Remove(category);
            await dbContext.SaveChangesAsync(cancellationToken);

            //category.AddDomainEvent(...)
        }
    }
}
