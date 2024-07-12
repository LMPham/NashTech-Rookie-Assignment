using Microsoft.Extensions.Logging;

namespace Application.UseCases.Products.EventHandlers;

public class ProductDeletedEventHandler : INotificationHandler<ProductDeletedEvent>
{
    private readonly ILogger<ProductDeletedEventHandler> logger;

    public ProductDeletedEventHandler(ILogger<ProductDeletedEventHandler> _logger)
    {
        logger = _logger;
    }

    public Task Handle(ProductDeletedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
