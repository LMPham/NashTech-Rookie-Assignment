using Microsoft.Extensions.Logging;

namespace Application.UseCases.Products.EventHandlers;

public class ProductCreatedEventHandler : INotificationHandler<ProductCreatedEvent>
{
    private readonly ILogger<ProductCreatedEventHandler> logger;

    public ProductCreatedEventHandler(ILogger<ProductCreatedEventHandler> _logger)
    {
        logger = _logger;
    }

    public Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
