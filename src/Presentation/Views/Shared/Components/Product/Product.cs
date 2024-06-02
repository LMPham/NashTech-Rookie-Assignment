namespace Presentation.Views.Shared.Components.Product
{
    public class Product : ViewComponent
    {
        public IViewComponentResult Invoke(ProductBriefDto product)
        {
            return View(product);
        }
    }
}
