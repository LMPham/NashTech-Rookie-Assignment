namespace Presentation.Models
{
    /// <summary>
    /// Model for ProductDetails-Index view.
    /// </summary>
    public record ProductDetailsIndexModel : BaseModel
    {
        public ProductDto Product { get; init; } = new ProductDto();
    }
}
