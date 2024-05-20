namespace Domain.Entities
{
    /// <summary>
    /// Category entity
    /// </summary>
    public class Category : BaseAuditableEntity<int>
    {
        public required string Name {  get; set; }
        public required string Description { get; set; }
    }
}
