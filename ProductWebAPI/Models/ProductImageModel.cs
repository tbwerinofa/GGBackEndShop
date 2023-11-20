using System.Text.Json.Serialization;

namespace ProductWebAPI.Models
{
    public class ProductImageModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }

        [JsonIgnore]
        public string? FileNameGuid { get; set; }
        [JsonIgnore]
        public string? FileName { get; set; }
        public IFormFile? File { get; set; }
        public string? UserId { get; internal set; }
    }
}
