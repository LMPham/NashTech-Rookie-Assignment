using FluentValidation;

namespace Application.UseCases.Products.Queries.GetProductsWithPagination
{
    public class GetProductsWithPaginationQueryValidator : AbstractValidator<GetProductsWithPaginationQuery>
    {
        /// <summary>
        /// Validator for getting Products with pagination.
        /// </summary>
        public GetProductsWithPaginationQueryValidator()
        {
            RuleFor(x => x.MinPrice)
                .GreaterThanOrEqualTo(0).WithMessage("MinPrice cannot be negative.");
            RuleFor(x => x.MaxPrice)
                .GreaterThanOrEqualTo(0).WithMessage("MaxPrice cannot be negative.");
            RuleFor(x => x.MaxPrice - x.MinPrice)
                .GreaterThanOrEqualTo(0).WithMessage("MaxPrice at least greater than or equal to MinPrice");
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");
            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
        }
    }
}
