namespace Domain.Common
{
    /// <summary>
    /// A class for summarizing the customer review scores
    /// of a <see cref="Product"/>.
    /// </summary>
    public class CustomerReviewScoreSummary
    {
        public int TotalReviews { get; set; }
        public double AverageScore { get; set; }
        public double FiveStarPercentage { get; set; }
        public double FourStarPercentage { get; set; }
        public double ThreeStarPercentage { get; set; }
        public double TwoStarPercentage { get; set; }
        public double OneStarPercentage { get; set; }
        public static CustomerReviewScoreSummary Summarize(Product product)
        {
            CustomerReviewScoreSummary summary = new CustomerReviewScoreSummary();

            List<CustomerReview> customerReviews = product.CustomerReviews;

            summary.TotalReviews = customerReviews.Count;

            if(customerReviews.Any())
            {
                summary.AverageScore = Math.Round(customerReviews.Average(cr => cr.Score), 1);
                summary.FiveStarPercentage = Math.Round((double) customerReviews.Count(cr => cr.Score == 5) / summary.TotalReviews * 100, 2);
                summary.FourStarPercentage = Math.Round((double)customerReviews.Count(cr => cr.Score == 4) / summary.TotalReviews * 100, 2);
                summary.ThreeStarPercentage = Math.Round((double) customerReviews.Count(cr => cr.Score == 3) / summary.TotalReviews * 100, 2);
                summary.TwoStarPercentage = Math.Round((double) customerReviews.Count(cr => cr.Score == 2) / summary.TotalReviews * 100, 2);
                summary.OneStarPercentage = Math.Round((double) customerReviews.Count(cr => cr.Score == 1) / summary.TotalReviews * 100, 2);
            }
            else
            {
                summary.AverageScore = 0.0;
                summary.FiveStarPercentage = 0.0;
                summary.FourStarPercentage = 0.0;
                summary.ThreeStarPercentage = 0.0;
                summary.TwoStarPercentage = 0.0;
                summary.OneStarPercentage = 0.0;
            }

            return summary;
        }
    }
}
