namespace Presentation.Models
{
    /// <summary>
    /// Model for SearchBar view component.
    /// </summary>
    public record SearchBarModel : BaseModel
    {
        public List<Department> departments { get; init; } = new List<Department>();
    }
}
