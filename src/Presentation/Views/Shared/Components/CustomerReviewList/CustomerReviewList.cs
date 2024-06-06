namespace Presentation.Views.Shared.Components.CustomerReviewList
{
    /// <summary>
    /// A view component for rendering a customer review list
    /// of the product displayed in the ProductDetail page.
    /// </summary>
    public class CustomerReviewList : ViewComponent
    {
        private readonly IUser user;

        public CustomerReviewList(IUser _user)
        {
            user = _user;
        }
        public IViewComponentResult Invoke(List<Domain.Entities.CustomerReview> customerReviews)
        {
            ViewData["user"] = user;
            return View(customerReviews);
        }
    }
}
