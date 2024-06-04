using FluentValidation;

namespace Application.UseCases.Products.Commands.GetProduct
{
    public class GetProductCommandValidation : AbstractValidator<GetProductCommand>
    {
        public GetProductCommandValidation()
        {
            //
        }
    }
}
