using Application.Common.Interfaces;
using Application.UseCases.Departments.Queries.GetDepartment;
using Ardalis.GuardClauses;

namespace Application.UseCases.Departments.Queries.GetDepartments;

/// <summary>
/// Request for getting existing Departments.
/// </summary>
public record GetDepartmentsQuery : IRequest<List<Department>>
{

}

/// <summary>
/// Request handler for getting existing Departments.
/// </summary>
public class GetDepartmentsQueryHandler : IRequestHandler<GetDepartmentsQuery, List<Department>>
{
    private readonly IApplicationDbContext dbContext;

    public GetDepartmentsQueryHandler(IApplicationDbContext _context)
    {
        dbContext = _context;
    }

    public async Task<List<Department>> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
    {
        return [.. dbContext.Departments
            .Include(d => d.Categories)
            .ThenInclude(c => c.Products)
            .ThenInclude(p => p.Details)
            .Include(d => d.Categories)
            .ThenInclude(c => c.Products)
            .ThenInclude(p => p.CustomerReviews)
            .Include(d => d.Categories)
            .ThenInclude(c => c.Products)
            .ThenInclude(p => p.Images)
            .OrderBy(d => d.Name)];
    }
}