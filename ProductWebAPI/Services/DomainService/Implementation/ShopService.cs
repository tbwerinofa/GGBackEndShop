using Microsoft.EntityFrameworkCore;
using ProductWebAPI.DataRepository;
using ProductWebAPI.Models;
using System.Text.Json.Serialization;

namespace ProductWebAPI.Services.DomainService.Implementation
{
    public class ShopService : IShopService
    {
        private readonly ProductDBContext _dbContext;

        public ShopService(ProductDBContext productDbContext)
        {
            _dbContext = productDbContext;
        }

        public async Task<IEnumerable<ShopModel>> GetModelList()
        {

            var entity = await _dbContext.Product
                .Include(b=>b.ProductImages)
                .ToListAsync();

            return entity.Select(a => new ShopModel
            {
                ProductId = a.Id,
                Name = a.Name,
                Code = a.Code,
                Price = a.Price,
                ProductImages = a.ProductImages.Select(a => new ProductImageListModel
                {
                    Id = a.Id,
                    ProductId = a.ProductId,
                    FileName = a.FileName,
                    FileNameGuid = a.FileNameGuid
                })
            });


        }

    }

    public class ProductImageListModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? FileNameGuid { get; set; }

        public string? FileName { get; set; }

    }
}
