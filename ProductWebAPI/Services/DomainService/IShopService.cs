using ProductWebAPI.Models;

namespace ProductWebAPI.Services.DomainService
{
    public interface IShopService
    {
        Task<IEnumerable<ShopModel>> GetModelList(string userId);
    }
}