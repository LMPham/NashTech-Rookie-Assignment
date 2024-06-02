namespace Domain.Entities
{
    /// <summary>
    /// Department entity.
    /// </summary>
    public class Department : BaseAuditableEntity<int>
    {
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;

        public List<Category> Categories { get; set; } = [];
        public List<Product> Products { get; set; } = [];
    }
}
