namespace ShopUI.Models
{
    public class ProductImageModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public IFormFile FormFile { get; set; }
    }
}
