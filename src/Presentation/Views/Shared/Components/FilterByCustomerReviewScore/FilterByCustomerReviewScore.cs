namespace Presentation.Views.Shared.Components.FilterByCustomerReviewScore
{
    /// <summary>
    /// A view component for rendering the filter-by-customer-review-score
    /// section in the Home page.
    /// </summary>
    public class FilterByCustomerReviewScore : ViewComponent
    {
        public IViewComponentResult Invoke(LookupDto queries)
        {
            return View(queries);
        }
    }
}
