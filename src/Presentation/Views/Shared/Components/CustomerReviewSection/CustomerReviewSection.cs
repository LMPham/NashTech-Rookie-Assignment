namespace Presentation.Views.Shared.Components.CustomerReviewSection
{
    /// <summary>
    /// A view component for rendering the customer reviews
    /// of the product displayed in the ProductDetail page.
    /// </summary>
    public class CustomerReviewSection : ViewComponent
    {
        private readonly IUser user;

        public CustomerReviewSection(IUser _user)
        {
            user = _user;
        }

        public IViewComponentResult Invoke(ProductDto product)
        {
            ViewData["product"] = product;
            ViewData["user"] = user;
            return View(); 
        }
    }
}
