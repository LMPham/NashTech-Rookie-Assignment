namespace Domain.Events.CustomerReviews
{
    public class CustomerReviewCreatedEvent : BaseEvent
    {
        public CustomerReviewCreatedEvent(CustomerReview customerReview)
        {
            CustomerReview = customerReview;
        }

        public CustomerReview CustomerReview { get; }
    }
}
