using FluentValidation;

namespace Application.UseCases.Departments.Queries.GetDepartments;

public class GetDepartmentsQueryValidator : AbstractValidator<GetDepartmentsQuery>
{
    /// <summary>
    /// Validator for getting existing Departments.
    /// </summary>
    public GetDepartmentsQueryValidator()
    {
        //
    }
}
