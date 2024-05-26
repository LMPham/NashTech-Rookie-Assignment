using FluentValidation;

namespace Application.UseCases.Products.Commands.UpdateProduct
{
    /// <summary>
    /// Validator for updating an existing Product.
    /// </summary>
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
    }
}
