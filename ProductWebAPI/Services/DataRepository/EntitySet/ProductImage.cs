using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductWebAPI.DataRepository
{
    public class ProductImage:Audit
    {
        public ProductImage()
        {
        }
        public int ProductId { get; set; }
        public string? FileNameGuid { get; set; }
        public string? FileName { get; set; }
        public string? UserId { get; set; }
        public virtual Product Product { get; set; }
    }
}
