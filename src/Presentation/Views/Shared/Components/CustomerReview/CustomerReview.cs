using Presentation.Services;

namespace Presentation.Views.Shared.Components.CustomerReview
{
    /// <summary>
    /// A view component for rendering a customer review
    /// of the product displayed in the ProductDetail page.
    /// </summary>
    public class CustomerReview : ViewComponent
    {
        private readonly IUser user;

        public CustomerReview(IUser _user)
        {
            user = _user;
        }

        public IViewComponentResult Invoke(Domain.Entities.CustomerReview customerReview)
        {
            ViewData["user"] = user;
            return View(customerReview);
        }
    }
}
