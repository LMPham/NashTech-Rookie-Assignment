namespace Domain.Events.CustomerReviews
{
    public class CustomerReviewDeletedEvent : BaseEvent
    {
        public CustomerReviewDeletedEvent(CustomerReview customerReview)
        {
            CustomerReview = customerReview;
        }

        public CustomerReview CustomerReview { get; }
    }
}
