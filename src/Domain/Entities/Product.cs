namespace Domain.Entities
{
    /// <summary>
    /// Product entity
    /// </summary>
    public class Product : BaseAuditableEntity<int>
    {
        public string Name { get; set; } = String.Empty;
        public Category Category { get; set; } = new Category();
        public string Description { get; set; } = String.Empty;
        public int Price {  get; set; } = 0;
        //public string? Image { get; set; }
    }
}
