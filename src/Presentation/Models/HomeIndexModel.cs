namespace Presentation.Models
{
    /// <summary>
    /// Model for Home-Index view.
    /// </summary>
    public record HomeIndexModel : BaseModel
    {
        public PaginatedList<ProductDto> Products { get; init; } = new PaginatedList<ProductDto>([], 0, 1, 50);
        
    }
}
