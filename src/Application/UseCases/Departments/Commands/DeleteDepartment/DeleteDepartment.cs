using Application.Common.Interfaces;
using Ardalis.GuardClauses;

namespace Application.UseCases.Departments.Commands.DeleteDepartment;

/// <summary>
/// Request to delete an existing Department.
/// </summary>
[Authorize(Roles = Roles.Administrator)]
public class DeleteDepartmentCommand : IRequest
{
    public required int Id { get; init; }
}

/// <summary>
/// Request handler for deleting an existing Department.
/// </summary>
public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand>
{
    private readonly IApplicationDbContext dbContext;

    public DeleteDepartmentCommandHandler(IApplicationDbContext _dbContext)
    {
        dbContext = _dbContext;
    }

    /// <summary>
    /// Finds the corresponding Department and removes it from the database.
    /// </summary>
    public async Task Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = dbContext.Departments.Where(d => d.Id == request.Id).FirstOrDefault();

        // Checks if the Category exists. If not, throws an exception
        Guard.Against.NotFound(request.Id, department);

        var categories = dbContext.Categories.Where(c => c.Department.Id == department.Id);
        dbContext.Categories.RemoveRange(categories);

        dbContext.Departments.Remove(department);

        department.AddDomainEvent(new DepartmentDeletedEvent(department));

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
