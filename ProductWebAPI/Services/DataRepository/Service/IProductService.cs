using Microsoft.EntityFrameworkCore;
using ProductWebAPI.Models;

namespace ProductWebAPI.Services.DataRepository.Service
{
    public interface IProductService
    {
        Task<Product> GetById(int productId);
        Task<SaveResult> Create(Product product);
        Task<SaveResult> Update(Product product);
        Task<SaveResult> Delete(int productId);
    }
}