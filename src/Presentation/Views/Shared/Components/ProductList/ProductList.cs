namespace Presentation.Views.Shared.Components.ProductList
{
    /// <summary>
    /// A view component for rendering a list of products displayed in
    /// the Home page.
    /// </summary>
    public class ProductList : ViewComponent
    {
        public IViewComponentResult Invoke(PaginatedList<ProductDto> products)
        {
            return View(products);
        }
    }
}
