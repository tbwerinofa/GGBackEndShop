using ProductWebAPI.Models;

namespace ProductWebAPI.Services.DocumentService
{
    public interface IDocumentManager
    {
        ProductImageModel UpLoad(NewProductImageModel productImage);

        Task<bool> Delete(string fileNameGuid);
    }
}
