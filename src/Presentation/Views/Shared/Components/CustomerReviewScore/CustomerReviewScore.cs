namespace Presentation.Views.Shared.Components.CustomerReviewScore
{
    /// <summary>
    /// A component for rendering customer review score in stars.
    /// </summary>
    public class CustomerReviewScore : ViewComponent
    {
        public IViewComponentResult Invoke(double score)
        {
            return View(score);
        }
    }
}
