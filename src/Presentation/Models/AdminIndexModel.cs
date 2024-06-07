namespace Presentation.Models
{
    public record AdminIndexModel : BaseModel
    {
        public List<Department> Departments { get; init; } = [];
    }
}
