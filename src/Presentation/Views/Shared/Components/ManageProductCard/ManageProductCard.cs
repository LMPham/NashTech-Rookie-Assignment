namespace Presentation.Views.Shared.Components.ManageProductCard
{
    public class ManageProductCard : ViewComponent
    {
        public IViewComponentResult Invoke(Product product)
        {
            return View(product);
        }
    }
}
