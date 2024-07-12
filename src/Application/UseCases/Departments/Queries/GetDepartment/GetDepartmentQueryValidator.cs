using FluentValidation;

namespace Application.UseCases.Departments.Queries.GetDepartment;

public class GetDepartmentQueryValidator : AbstractValidator<GetDepartmentQuery>
{
    /// <summary>
    /// Validator for getting a Department.
    /// </summary>
    public GetDepartmentQueryValidator()
    {
        //
    }
}
