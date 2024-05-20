using Application.Common.Interfaces;
using Ardalis.GuardClauses;

namespace Application.UseCases.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest
    {
        public required int Id { get; init; }
        public string? Name { get; init; }
        public string? Description { get; init; }

    }

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
    {
        private readonly IApplicationDbContext dbContext;

        public UpdateCategoryCommandHandler(IApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = dbContext.Categories.Where(c => c.Id == request.Id).FirstOrDefault();

            Guard.Against.NotFound(request.Id, category);

            category.Name = request.Name ?? category.Name;
            category.Description = request.Description ?? category.Description;
            await dbContext.SaveChangesAsync(cancellationToken);

            //category.AddDomainEvent(...)
        }
    }
}
