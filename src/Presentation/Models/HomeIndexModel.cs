namespace Presentation.Models
{
    /// <summary>
    /// Model for Home-Index view.
    /// </summary>
    public record HomeIndexModel : BaseModel
    {
        public PaginatedList<ProductBriefDto> Products { get; init; } = new PaginatedList<ProductBriefDto>([], 0, 1, 50);
        
    }
}
