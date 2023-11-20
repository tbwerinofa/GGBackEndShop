using ProductWebAPI.Services.DomainService.Implementation;
using System.ComponentModel.DataAnnotations;

namespace ProductWebAPI.Models
{
    public class ShopModel
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public decimal Price { get; set; }

        public IEnumerable<ProductImageListModel>? ProductImages { get; set; }

    }
}
