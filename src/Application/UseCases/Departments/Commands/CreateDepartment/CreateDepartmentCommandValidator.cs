using FluentValidation;

namespace Application.UseCases.Departments.Commands.CreateDepartment
{
    /// <summary>
    /// Validator for creating a new Department.
    /// </summary>
    public class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
    {
        public CreateDepartmentCommandValidator()
        {
            //
        }
    }
}
