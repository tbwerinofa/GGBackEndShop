using ProductWebAPI.DataRepository;
using ProductWebAPI.Models;

namespace ProductWebAPI.Services.DataRepository.Service.Implementation
{
    public class ProductService:IProductService
    {
        private readonly ProductDBContext _dbContext;

        public ProductService(ProductDBContext productDbContext)
        {
            _dbContext = productDbContext;
        }

 
        public async Task<Product> GetById(int productId)
        {
            var product = await _dbContext.Product.FindAsync(productId);
            return product;
        }

        public async Task<SaveResult> Create(Product product)
        {
            SaveResult saveResult = new SaveResult();
            try
            {
                await _dbContext.Product.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                saveResult.Message = "Error Saving Record";
            }

            return saveResult;
        }


        public async Task<SaveResult> Update(Product product)
        {
            SaveResult saveResult = new SaveResult();
            try
            {
                _dbContext.Product.Update(product);
            await _dbContext.SaveChangesAsync();
            saveResult.IsSuccess = true;
        }
            catch (Exception)
            {
                saveResult.Message = "Error Saving Record";
            }
          
            return saveResult;
        }


        public async Task<SaveResult> Delete(int productId)
        {
            SaveResult saveResult = new SaveResult();
            try
            {
                var product = await _dbContext.Product.FindAsync(productId);
                _dbContext.Product.Remove(product);
                await _dbContext.SaveChangesAsync();
                saveResult.IsSuccess = true;
            }
            catch (Exception)
            {
                saveResult.Message = "Error Saving Record";
            }
          
            return saveResult;
        }
    }
}
