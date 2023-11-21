using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProductWebAPI.Models
{
    public class ProductModel
    {
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "name is required")]
        [StringLength(100, ErrorMessage = "must be less than 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "code is required")]
        [StringLength(20, ErrorMessage = "must be less than 20 characters")]
        public string Code { get; set; }
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "price must be greater than 0")]
        public decimal Price { get; set; }

        [JsonIgnore]
        public string? UserId { get; set; }
    }
}
