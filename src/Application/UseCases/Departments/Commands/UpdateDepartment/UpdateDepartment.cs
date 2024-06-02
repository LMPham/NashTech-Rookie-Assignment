using Application.Common.Interfaces;
using Ardalis.GuardClauses;

namespace Application.UseCases.Departments.Commands.UpdateDepartment
{
    /// <summary>
    /// Request to update an existing Department.
    /// </summary>
    public class UpdateDepartmentCommand : IRequest
    {
        public required int Id { get; init; }
        public string? Name { get; init; }
        public string? Description { get; init; }
    }

    /// <summary>
    /// Request handler for updating an existing Department.
    /// </summary>
    public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand>
    {
        private readonly IApplicationDbContext dbContext;

        public UpdateDepartmentCommandHandler(IApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        /// <summary>
        /// Finds the corresponding Department and updates it.
        /// Throws an exception if the Department does not exist.
        /// </summary>
        public async Task Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = dbContext.Departments.Where(d => d.Id == request.Id).FirstOrDefault();

            // Checks if the department exist. If not, throws an exception
            Guard.Against.NotFound(request.Id, department);

            department.Name = request.Name ?? department.Name;

            department.Description = request.Description ?? department.Description;

            department.AddDomainEvent(new DepartmentUpdatedEvent(department));

            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
