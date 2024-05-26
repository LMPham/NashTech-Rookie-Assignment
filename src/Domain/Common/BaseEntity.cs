using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common
{
    /// <summary>
    /// Base class for domain entities.
    /// </summary>
    public abstract class BaseEntity<T>
    {
        public T? Id { get; set; }
        private readonly List<BaseEvent> _domainEvents = new();

        [NotMapped]
        public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

        /// <summary>
        /// Adds a domain event to the entity.
        /// </summary>
        public void AddDomainEvent(BaseEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        /// <summary>
        /// Removes a domain event from the entity.
        /// </summary>
        public void RemoveDomainEvent(BaseEvent domainEvent)
        {
            _domainEvents.Remove(domainEvent);
        }

        /// <summary>
        /// Removes all domain events from the entity.
        /// </summary>
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
