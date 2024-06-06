using Application.Common.Interfaces;
using Ardalis.GuardClauses;

namespace Application.UseCases.Categories.Commands.CreateCategory
{
    /// <summary>
    /// Request to create a new Category.
    /// </summary>
    [Authorize(Roles = Roles.Administrator)]
    public record CreateCategoryCommand : IRequest<int>
    {
        public required string Name { get; init; }
        public string Description { get; init; } = String.Empty;
        public required Department Department { get; init; }
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
        /// The Category must be in an existing department
        /// </summary>
        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            // Checks if the Department exists in the database.
            var department = dbContext.Departments.Where(d => d.Id == request.Department.Id).FirstOrDefault();
            Guard.Against.NotFound(request.Department.Id, department);

            var category = new Category
            {
                Name = request.Name,
                Description = request.Description,
            };

            category.AddDomainEvent(new CategoryCreatedEvent(category));

            department.Categories.Add(category);

            await dbContext.SaveChangesAsync(cancellationToken);

            return category.Id;
        }
    }
}
