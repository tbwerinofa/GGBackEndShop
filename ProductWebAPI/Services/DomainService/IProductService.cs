using Microsoft.EntityFrameworkCore;
using ProductWebAPI.Models;

namespace ProductWebAPI.Services.DomainService
{
    public interface IProductService
    {
        Task<ProductModel> GetById(int productId);
        Task<SaveResult> Create(NewProductModel product);
        Task<SaveResult> Update(ProductModel product);
        Task<SaveResult> Delete(int productId);
        Task<IEnumerable<ProductModel>> GetModelList();
    }
}