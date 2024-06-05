using FluentValidation;

namespace Application.UseCases.Products.Commands.GetProduct
{
    public class GetProductCommandValidator : AbstractValidator<GetProductCommand>
    {
        public GetProductCommandValidator()
        {
            //
        }
    }
}
