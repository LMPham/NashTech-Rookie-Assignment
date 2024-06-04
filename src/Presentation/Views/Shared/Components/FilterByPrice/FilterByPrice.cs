namespace Presentation.Views.Shared.Components.FilterByPrice
{
    /// <summary>
    /// A view component for rendering the filter-by-price section
    /// in the Home page.
    /// </summary>
    public class FilterByPrice : ViewComponent
    {
        public IViewComponentResult Invoke(LookupDto queries)
        {
            return View(queries);
        }
    }
}
