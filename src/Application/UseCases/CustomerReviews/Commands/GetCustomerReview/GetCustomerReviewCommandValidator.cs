using FluentValidation;

namespace Application.UseCases.CustomerReviews.Commands.GetCustomerReview
{
    public class GetCustomerReviewCommandValidator : AbstractValidator<GetCustomerReviewCommand>
    {
        public GetCustomerReviewCommandValidator()
        {
            //
        }
    }
}
