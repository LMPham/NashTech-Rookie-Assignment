using FluentValidation;

namespace Application.UseCases.CustomerReviews.Commands.GetCustomerReviewsWithPagination
{
    public class GetCustomerReviewsWithPaginationCommandValidator : AbstractValidator<GetCustomerReviewsWithPaginationCommand>
    {
        public GetCustomerReviewsWithPaginationCommandValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");
            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
        }
    }
}
