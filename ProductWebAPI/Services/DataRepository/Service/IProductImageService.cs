using ProductWebAPI.Models;

namespace ProductWebAPI.Services.DataRepository.Service
{
    public interface IProductImageService
    {
        Task<ProductImageModel> GetById(int Id);
        Task<SaveResult> Save(ProductImageModel model);
        Task<SaveResult> Delete(int productId);
    }
}