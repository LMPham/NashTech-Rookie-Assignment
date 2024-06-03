namespace Domain.Entities
{
    /// <summary>
    /// Category entity.
    /// </summary>
    public class Category : BaseAuditableEntity<int>
    {
        public string Name { get; set; } = String.Empty;
        public Department Department { get; set; } = new Department();
        public string Description { get; set; } = String.Empty;
    }
}
