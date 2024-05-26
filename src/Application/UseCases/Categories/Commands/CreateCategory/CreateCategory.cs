using Application.Common.Interfaces;

namespace Application.UseCases.Categories.Commands.CreateCategory
{
    /// <summary>
    /// Request to create a new Category.
    /// </summary>
    public record CreateCategoryCommand : IRequest<int>
    {
        public required string Name { get; init; }
        public string Description { get; init; } = String.Empty;
    }

    /// <summary>
    /// Request handler for creating a new Category.
    /// </summary>
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
    {
        private readonly IApplicationDbContext dbContext;

        public CreateCategoryCommandHandler(IApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        /// <summary>
        /// Creates a new Category and adds it into the database.
        /// </summary>
        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Name = request.Name,
                Description = request.Description,
            };

            dbContext.Categories.Add(category);
            await dbContext.SaveChangesAsync(cancellationToken);

            //category.AddDomainEvent(...)

            return category.Id;
        }
    }
}
