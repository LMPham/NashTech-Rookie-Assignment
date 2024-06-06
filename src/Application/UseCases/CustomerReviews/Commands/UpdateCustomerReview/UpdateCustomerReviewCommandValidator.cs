using FluentValidation;

namespace Application.UseCases.CustomerReviews.Commands.UpdateCustomerReview
{
    public class UpdateCustomerReviewCommandValidator : AbstractValidator<UpdateCustomerReviewCommand>
    {
        /// <summary>
        /// Validator for updating an existing CustomerReview.
        /// </summary>
        public UpdateCustomerReviewCommandValidator()
        {
            //
        }
    }
}
