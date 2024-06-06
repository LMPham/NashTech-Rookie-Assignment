using Microsoft.Extensions.Logging;

namespace Application.UseCases.CustomerReviews.EventHandlers
{
    public class CustomerReviewDeletedEventHandler : INotificationHandler<CustomerReviewDeletedEvent>
    {
        private readonly ILogger<CustomerReviewDeletedEventHandler> logger;

        public CustomerReviewDeletedEventHandler(ILogger<CustomerReviewDeletedEventHandler> _logger)
        {
            logger = _logger;
        }

        public Task Handle(CustomerReviewDeletedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
