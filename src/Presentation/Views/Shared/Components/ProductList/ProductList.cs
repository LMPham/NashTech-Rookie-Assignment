using Application.Common.Models;

namespace Presentation.Views.Shared.Components.ProductList
{
    public class ProductList : ViewComponent
    {
        public IViewComponentResult Invoke(PaginatedList<ProductBriefDto> products)
        {
            return View(products);
        }
    }
}
