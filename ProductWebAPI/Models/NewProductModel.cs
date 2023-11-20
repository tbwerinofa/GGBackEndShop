using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductWebAPI.Models
{
    public class NewProductModel
    {

        [Required(ErrorMessage = "required")]
        [StringLength(100, ErrorMessage = "must be less than 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "required")]
        [StringLength(20, ErrorMessage = "must be less than 20 characters")]
        public string Code { get; set; }
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public decimal Price { get; set; }
        //public string UserId { get; set; }
    }
}
