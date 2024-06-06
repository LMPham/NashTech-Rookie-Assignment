namespace Domain.Entities
{
    /// <summary>
    /// Product entity.
    /// </summary>
    public class Product : BaseAuditableEntity<int>
    {
        public string Name { get; set; } = String.Empty;
        public Department Department { get; set; } = new Department();
        public Category Category { get; set; } = new Category();
        public List<string> Descriptions { get; set; } = [];
        public List<ProductDetail> Details { get; set; } = [];
        public List<CustomerReview> CustomerReviews { get; set; } = new List<CustomerReview>();
        public int Quantity { get; set; }
        public int Price {  get; set; } = 0;
        //public string? Image { get; set; }
    }
}
