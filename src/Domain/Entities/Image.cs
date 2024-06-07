namespace Domain.Entities
{
    public class Image : BaseEntity<int>
    {
        public string Name { get; set; } = String.Empty;
        public byte[] Data { get; set; } = [];
        public int ProductId { get; set; }
    }
}
