using Microsoft.Extensions.Logging;

namespace Application.UseCases.CustomerReviews.EventHandlers
{
    public class CustomerReviewCreatedEventHandler : INotificationHandler<CustomerReviewCreatedEvent>
    {
        private readonly ILogger<CustomerReviewCreatedEventHandler> logger;

        public CustomerReviewCreatedEventHandler(ILogger<CustomerReviewCreatedEventHandler> _logger)
        {
            logger = _logger;
        }

        public Task Handle(CustomerReviewCreatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
