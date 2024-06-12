using FluentValidation;

namespace Application.UseCases.Products.Queries.GetProduct
{
    public class GetProductQueryValidator : AbstractValidator<GetProductQuery>
    {
        /// <summary>
        /// Validator for getting a Product.
        /// </summary>
        public GetProductQueryValidator()
        {
            //
        }
    }
}
