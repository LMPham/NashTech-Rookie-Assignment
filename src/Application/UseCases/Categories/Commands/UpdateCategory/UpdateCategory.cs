using Application.Common.Interfaces;
using Ardalis.GuardClauses;

namespace Application.UseCases.Categories.Commands.UpdateCategory
{
    /// <summary>
    /// Request to update an existing Category.
    /// </summary>
    public class UpdateCategoryCommand : IRequest
    {
        public required int Id { get; init; }
        public string? Name { get; init; }
        public string? Description { get; init; }

    }

    /// <summary>
    /// Request handler for updating an existing Category.
    /// </summary>
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
    {
        private readonly IApplicationDbContext dbContext;

        public UpdateCategoryCommandHandler(IApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        /// <summary>
        /// Finds the corresponding Category and updates it.
        /// Throws an exception if the Category does not exist.
        /// </summary>

        public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = dbContext.Categories.Where(c => c.Id == request.Id).FirstOrDefault();

            // Checks if the Category exists. If not, throws an exception
            Guard.Against.NotFound(request.Id, category);

            category.Name = request.Name ?? category.Name;
            category.Description = request.Description ?? category.Description;
            await dbContext.SaveChangesAsync(cancellationToken);

            //category.AddDomainEvent(...)
        }
    }
}
