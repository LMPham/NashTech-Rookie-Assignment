namespace Domain.Common
{
    /// <summary>
    /// Base class for entities that can be audited.
    /// </summary>
    public abstract class BaseAuditableEntity<T> : BaseEntity<T>
    {
        public DateTimeOffset Created { get; set; }

        public string? CreatedBy { get; set; }

        public DateTimeOffset LastModified { get; set; }

        public string? LastModifiedBy { get; set; }
    }
}
