using Application.Common.Interfaces;
using Application.UseCases.Products.Commands.CreateProduct;

namespace Application.UseCases.Categories.Commands.CreateCategory
{
    public record CreateCategoryCommand : IRequest<int>
    {
        public required string Name { get; init; }
        public string Description { get; init; } = String.Empty;
    }

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
    {
        private readonly IApplicationDbContext dbContext;

        public CreateCategoryCommandHandler(IApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

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
