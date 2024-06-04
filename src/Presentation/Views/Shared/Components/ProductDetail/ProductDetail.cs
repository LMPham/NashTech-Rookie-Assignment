namespace Presentation.Views.Shared.Components.ProductDetail
{
    /// <summary>
    /// A view component for displaying the product displayed in
    /// the ProductDetail page.
    /// </summary>
    public class ProductDetail : ViewComponent
    {
        public IViewComponentResult Invoke(ProductBriefDto product)
        {
            return View(product);
        }
    }
}
