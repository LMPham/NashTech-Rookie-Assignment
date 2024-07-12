using Microsoft.Extensions.Logging;

namespace Application.UseCases.Departments.EventHandlers;

public class DepartmentCreatedEventHandler : INotificationHandler<DepartmentCreatedEvent>
{
    private readonly ILogger<DepartmentCreatedEventHandler> logger;

    public DepartmentCreatedEventHandler(ILogger<DepartmentCreatedEventHandler> _logger)
    {
        logger = _logger;
    }

    public Task Handle(DepartmentCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
