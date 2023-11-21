using Microsoft.EntityFrameworkCore;
using ProductWebAPI.Models;

namespace ProductWebAPI.Services.DomainService
{
    public interface IProductService
    {
        Task<ProductModel> GetById(int productId, string userId);
        Task<SaveResult> Create(NewProductModel product);
        Task<SaveResult> Update(ProductModel product);
        Task<SaveResult> Delete(int productId,string userId);
        Task<IEnumerable<ProductModel>> GetModelList(string userId);
    }
}