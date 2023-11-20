using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProductWebAPI.Models
{
    public class NewProductImageModel
    {
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "productid is required")]
        public int ProductId { get; set; }
        public IFormFile? File { get; set; }

    }
}
