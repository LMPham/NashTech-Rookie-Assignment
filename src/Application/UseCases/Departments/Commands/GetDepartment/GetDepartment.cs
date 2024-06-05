using Application.Common.Interfaces;
using Ardalis.GuardClauses;
namespace Application.UseCases.Departments.Commands.GetDepartment
{
    /// <summary>
    /// Request for getting an existing Department.
    /// </summary>
    public class GetDepartmentCommand : IRequest<Department>
    {
        public required int Id { get; init; }
    }

    /// <summary>
    /// Request handler for getting an existing Department.
    /// </summary>
    public class GetDepartmentCommandHandler : IRequestHandler<GetDepartmentCommand, Department>
    {
        private readonly IApplicationDbContext dbContext;

        public GetDepartmentCommandHandler(IApplicationDbContext _context)
        {
            dbContext = _context;
        }

        public async Task<Department> Handle(GetDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = dbContext.Departments.Where(d => d.Id == request.Id).FirstOrDefault();

            // Checks if the Department exists. If not, throws an exception
            Guard.Against.NotFound(request.Id, department);

            return department;
        }
    }
}
