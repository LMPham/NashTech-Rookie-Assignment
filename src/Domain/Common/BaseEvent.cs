using MediatR;

namespace Domain.Common
{
    /// <summary>
    /// Base class for domain events.
    /// </summary>
    public abstract class BaseEvent : INotification
    {
    }
}
