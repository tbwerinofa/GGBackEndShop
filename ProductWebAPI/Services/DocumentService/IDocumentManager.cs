using ProductWebAPI.Models;

namespace ProductWebAPI.Services.DocumentService
{
    public interface IDocumentManager
    {
        bool UpLoad(ProductImageModel productImage);
    }
}
