using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Domain.Common
{
    /// <summary>
    /// Base class for domain entities.
    /// </summary>
    public abstract class BaseEntity<T> : IEquatable<BaseEntity<T>>
    {
        public T? Id { get; set; }
        private readonly List<BaseEvent> domainEvents = new();

        [NotMapped]
        public IReadOnlyCollection<BaseEvent> DomainEvents => domainEvents.AsReadOnly();

        /// <summary>
        /// Adds a domain event to the entity.
        /// </summary>
        public void AddDomainEvent(BaseEvent domainEvent)
        {
            domainEvents.Add(domainEvent);
        }

        /// <summary>
        /// Removes a domain event from the entity.
        /// </summary>
        public void RemoveDomainEvent(BaseEvent domainEvent)
        {
            domainEvents.Remove(domainEvent);
        }

        /// <summary>
        /// Removes all domain events from the entity.
        /// </summary>
        public void ClearDomainEvents()
        {
            domainEvents.Clear();
        }

        public bool Equals(BaseEntity<T>? other)
        {
            //Check whether the compared object is null. 
            if (other == null)
            {
                return false;
            }

            //Check whether the compared object references the same data. 
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            // Check if both entities have ids
            if (Id == null || other.Id == null)
            {
                return true;
            }
            //Check whether the entities' ids are equal.
            return Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return Id == null ? 0 : Id.GetHashCode();
        }
    }
}
