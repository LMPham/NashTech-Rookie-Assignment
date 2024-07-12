using FluentValidation;

namespace Application.UseCases.CustomerReviews.Queries.GetCustomerReview;

public class GetCustomerReviewQueryValidator : AbstractValidator<GetCustomerReviewQuery>
{
    /// <summary>
    /// Validator for getting a CustomerReview.
    /// </summary>
    public GetCustomerReviewQueryValidator()
    {
        //
    }
}
