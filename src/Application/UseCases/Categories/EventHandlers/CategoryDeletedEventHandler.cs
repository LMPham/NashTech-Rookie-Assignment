using Microsoft.Extensions.Logging;

namespace Application.UseCases.Categories.EventHandlers;

public class CategoryDeletedEventHandler : INotificationHandler<CategoryDeletedEvent>
{
    private readonly ILogger<CategoryDeletedEventHandler> logger;

    public CategoryDeletedEventHandler(ILogger<CategoryDeletedEventHandler> _logger)
    {
        logger = _logger;
    }

    public Task Handle(CategoryDeletedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
