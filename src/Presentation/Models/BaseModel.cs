namespace Presentation.Models
{
    /// <summary>
    /// Base model.
    /// </summary>
    public record BaseModel
    {
        public LookupDto Queries { get; set; } = new LookupDto();
        public required IUser User { get; init; }
    }
}
