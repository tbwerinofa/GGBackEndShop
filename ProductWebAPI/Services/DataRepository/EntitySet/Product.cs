using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Minio.DataModel;


namespace ProductWebAPI.DataRepository
{
    public class Product: Audit
    {
        public Product()
        {
            this.ProductImages = new HashSet<ProductImage>();
        }

        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public string UserId { get; internal set; }
    }
}
