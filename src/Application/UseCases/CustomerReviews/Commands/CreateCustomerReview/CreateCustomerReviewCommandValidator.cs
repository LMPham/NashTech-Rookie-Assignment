using FluentValidation;

namespace Application.UseCases.CustomerReviews.Commands.CreateCustomerReview;

/// <summary>
/// Validator for creating a new CustomerReview.
/// </summary>
public class CreateCustomerReviewCommandValidator : AbstractValidator<CreateCustomerReviewCommand>
{
    public CreateCustomerReviewCommandValidator()
    {
        //
    }
}
