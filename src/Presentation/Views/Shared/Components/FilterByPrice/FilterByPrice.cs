namespace Presentation.Views.Shared.Components.FilterByPrice
{
    public class FilterByPrice : ViewComponent
    {
        public IViewComponentResult Invoke(LookupDto queries)
        {
            return View(queries);
        }
    }
}
