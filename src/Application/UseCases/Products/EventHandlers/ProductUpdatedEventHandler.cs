using Microsoft.Extensions.Logging;

namespace Application.UseCases.Products.EventHandlers
{
    public class ProductUpdatedEventHandler : INotificationHandler<ProductUpdatedEvent>
    {
        private readonly ILogger<ProductUpdatedEventHandler> logger;

        public ProductUpdatedEventHandler(ILogger<ProductUpdatedEventHandler> _logger)
        {
            logger = _logger;
        }

        public Task Handle(ProductUpdatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
