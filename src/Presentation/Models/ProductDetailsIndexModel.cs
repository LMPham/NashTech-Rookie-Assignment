namespace Presentation.Models
{
    /// <summary>
    /// Model for ProductDetails-Index view.
    /// </summary>
    public record ProductDetailsIndexModel : BaseModel
    {
        public ProductBriefDto Product { get; init; } = new ProductBriefDto();
    }
}
