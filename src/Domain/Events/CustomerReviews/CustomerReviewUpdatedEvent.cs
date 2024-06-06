namespace Domain.Events.CustomerReviews
{
    public class CustomerReviewUpdatedEvent : BaseEvent
    {
        public CustomerReviewUpdatedEvent(CustomerReview customerReview)
        {
            CustomerReview = customerReview;
        }

        public CustomerReview CustomerReview { get; }
    }
}
