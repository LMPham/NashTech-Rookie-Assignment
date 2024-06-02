namespace Presentation.Views.Shared.Components.CustomerReviewScore
{
    public class CustomerReviewScore : ViewComponent
    {
        public IViewComponentResult Invoke(double score)
        {
            return View(score);
        }
    }
}
