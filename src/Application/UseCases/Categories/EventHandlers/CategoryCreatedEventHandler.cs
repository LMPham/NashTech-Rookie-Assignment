using Microsoft.Extensions.Logging;

namespace Application.UseCases.Categories.EventHandlers;

public class CategoryCreatedEventHandler : INotificationHandler<CategoryCreatedEvent>
{
    private readonly ILogger<CategoryCreatedEventHandler> logger;

    public CategoryCreatedEventHandler(ILogger<CategoryCreatedEventHandler> _logger)
    {
        logger = _logger;
    }

    public Task Handle(CategoryCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
