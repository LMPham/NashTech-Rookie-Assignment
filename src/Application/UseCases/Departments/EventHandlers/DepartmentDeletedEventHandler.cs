using Microsoft.Extensions.Logging;

namespace Application.UseCases.Departments.EventHandlers
{
    public class DepartmentDeletedEventHandler : INotificationHandler<DepartmentDeletedEvent>
    {
        private readonly ILogger<DepartmentDeletedEventHandler> logger;

        public DepartmentDeletedEventHandler(ILogger<DepartmentDeletedEventHandler> _logger)
        {
            logger = _logger;
        }

        public Task Handle(DepartmentDeletedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
