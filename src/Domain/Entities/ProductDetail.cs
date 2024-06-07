namespace Domain.Entities
{
    /// <summary>
    /// Stores the details of a <see cref="Product"/>.
    /// </summary>
    public class ProductDetail : BaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int ProductId { get; set; }
    }
}
