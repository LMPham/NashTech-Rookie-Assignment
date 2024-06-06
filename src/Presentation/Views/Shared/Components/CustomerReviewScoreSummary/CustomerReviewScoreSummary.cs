namespace Presentation.Views.Shared.Components.CustomerReviewScoreSummary
{
    /// <summary>
    /// A view component for rendering the summary of customer
    /// review scores of the product displayed in the ProductDetail page.
    /// </summary>
    public class CustomerReviewScoreSummary : ViewComponent
    {
        public IViewComponentResult Invoke(Domain.Common.CustomerReviewScoreSummary scoreSummary)
        {
            return View(scoreSummary);
        }
    }
}
