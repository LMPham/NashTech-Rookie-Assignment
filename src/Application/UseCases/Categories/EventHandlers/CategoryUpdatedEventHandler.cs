using Domain.Events.Categories;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Categories.EventHandlers
{
    public class CategoryUpdatedEventHandler : INotificationHandler<CategoryUpdatedEvent>
    {
        private readonly ILogger<CategoryUpdatedEventHandler> logger;

        public CategoryUpdatedEventHandler(ILogger<CategoryUpdatedEventHandler> _logger)
        {
            logger = _logger;
        }

        public Task Handle(CategoryUpdatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
