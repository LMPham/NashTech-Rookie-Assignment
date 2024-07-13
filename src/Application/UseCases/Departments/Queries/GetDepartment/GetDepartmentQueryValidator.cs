using FluentValidation;

namespace Application.UseCases.Departments.Queries.GetDepartment;

public class GetDepartmentQueryValidator : AbstractValidator<GetDepartmentQuery>
{
    /// <summary>
    /// Validator for getting an existing Department.
    /// </summary>
    public GetDepartmentQueryValidator()
    {
        //
    }
}
