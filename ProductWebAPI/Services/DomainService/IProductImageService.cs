using ProductWebAPI.Models;

namespace ProductWebAPI.Services.DomainService
{
    public interface IProductImageService
    {
        Task<ProductImageModel> GetById(int Id, string userId);
        Task<SaveResult> Save(ProductImageModel model);
        Task<SaveResult> Delete(int productId);
    }
}