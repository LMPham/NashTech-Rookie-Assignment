namespace Presentation.Views.Shared.Components.FilterByCustomerReviewScore
{
    public class FilterByCustomerReviewScore : ViewComponent
    {
        public IViewComponentResult Invoke(LookupDto queries)
        {
            return View(queries);
        }
    }
}
