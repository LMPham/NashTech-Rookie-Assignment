namespace Presentation.Views.Shared.Components.ProductCard
{
    /// <summary>
    /// A view component for rendering a product card.
    /// </summary>
    public class ProductCard : ViewComponent
    {
        public IViewComponentResult Invoke(ProductDto product)
        {
            return View(product);
        }
    }
}
