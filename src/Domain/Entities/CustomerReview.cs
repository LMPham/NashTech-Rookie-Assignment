using System.Text.Json.Serialization;

namespace Domain.Entities
{
    /// <summary>
    /// Customer review entity.
    /// </summary>
    public class CustomerReview : BaseAuditableEntity<int>
    {
        public int Score { get; set; }
        public string Headline { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        [JsonIgnore]
        public Product Product { get; set; } = new Product();

    }
}
