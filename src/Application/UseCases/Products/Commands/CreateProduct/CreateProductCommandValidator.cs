using FluentValidation;

namespace Application.UseCases.Products.Commands.CreateProduct;

/// <summary>
/// Validator for creating a new Product.
/// </summary>
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0);
    }
}
