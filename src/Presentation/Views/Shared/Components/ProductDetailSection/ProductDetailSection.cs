namespace Presentation.Views.Shared.Components.ProductDetail
{
    /// <summary>
    /// A view component for rendering the product displayed in
    /// the ProductDetail page.
    /// </summary>
    public class ProductDetailSection : ViewComponent
    {
        public IViewComponentResult Invoke(ProductDto product)
        {
            return View(product);
        }
    }
}
