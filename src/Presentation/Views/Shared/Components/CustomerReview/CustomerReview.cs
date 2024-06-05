namespace Presentation.Views.Shared.Components.CustomerReview
{
    /// <summary>
    /// A view component for rendering a customer review
    /// of the product displayed in the ProductDetail page.
    /// </summary>
    public class CustomerReview : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
