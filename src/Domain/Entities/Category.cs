namespace Domain.Entities
{
    /// <summary>
    /// Category entity
    /// </summary>
    public class Category : BaseEntity<int>
    {
        public required string Name {  get; set; }
        public required string Description { get; set; }
    }
}
