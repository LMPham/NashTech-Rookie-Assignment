
using Application.Common.Interfaces;

namespace Application.UseCases.Products.Commands.CreateProduct
{
    public record CreateProductCommand : IRequest<int>
    {
        public required string Name { get; init; }
        public required Category Category { get; init; }
        public string? Description { get; init; }
        public required int Price { get; init; }
        //public string? Image { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IApplicationDbContext dbContext;

        public CreateProductCommandHandler(IApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = new Product
            {
                Name = request.Name,
                Category = request.Category,
                Description = request.Description,
                Price = request.Price,
            };

            //entity.AddDomainEvent(...)

            //dbContext.Products.Add(entity);
            //await dbContext.SaveChangesAsync(cancellationToken);

            //return entity.Id;
            Console.WriteLine(entity.Name);
            Console.WriteLine(entity.Category);
            Console.WriteLine(entity.Description);
            Console.WriteLine(entity.Price);
            return 1000000;
        }
    }
}
