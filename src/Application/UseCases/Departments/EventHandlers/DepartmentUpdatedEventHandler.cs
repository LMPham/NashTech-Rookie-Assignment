using Microsoft.Extensions.Logging;

namespace Application.UseCases.Departments.EventHandlers;

public class DepartmentUpdatedEventHandler : INotificationHandler<DepartmentUpdatedEvent>
{
    private readonly ILogger<DepartmentUpdatedEventHandler> logger;

    public DepartmentUpdatedEventHandler(ILogger<DepartmentUpdatedEventHandler> _logger)
    {
        logger = _logger;
    }

    public Task Handle(DepartmentUpdatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
