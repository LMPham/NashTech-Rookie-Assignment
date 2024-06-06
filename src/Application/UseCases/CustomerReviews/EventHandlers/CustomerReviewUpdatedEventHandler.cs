using Microsoft.Extensions.Logging;

namespace Application.UseCases.CustomerReviews.EventHandlers
{
    public class CustomerReviewUpdatedEventHandler : INotificationHandler<CustomerReviewUpdatedEvent>
    {
        private readonly ILogger<CustomerReviewUpdatedEventHandler> logger;

        public CustomerReviewUpdatedEventHandler(ILogger<CustomerReviewUpdatedEventHandler> _logger)
        {
            logger = _logger;
        }

        public Task Handle(CustomerReviewUpdatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
