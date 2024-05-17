namespace Domain.Entities
{
    /// <summary>
    /// Product entity
    /// </summary>
    public class Product : BaseAuditableEntity<int>
    {
        public required string Name { get; set; }
        public required Category Category { get; set; }
        public required string Description { get; set; }
        public required int Price {  get; set; }
        //public string? Image { get; set; }
    }
}
