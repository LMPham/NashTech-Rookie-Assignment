using Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest
    {
        public required int Id { get; init; }
        public required string Name { get; set; }
        public required string Description { get; set; }

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

        }
    }
}
