using FluentValidation;

namespace Application.UseCases.Categories.Commands.CreateCategory;

/// <summary>
/// Validator for creating a new Category.
/// </summary>
public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        //
    }
}
