using ShopUI.Models;
using System.Net.Http.Formatting;

namespace ShopUI.APIClient
{
    public interface IProductAPIClient
    {
        Task<IEnumerable<ProductModel>> GetModelList(string token);
        Task<ProductModel> GetModelById(int id);

        Task<bool> Create(ProductModel model);

        Task<bool> Update(ProductModel model);

        Task<bool> DeleteById(int id);
    }
}