using Microsoft.EntityFrameworkCore;
using ProductWebAPI.DataRepository;
using ProductWebAPI.Models;
using ProductWebAPI.Services.DomainService;

namespace ProductWebAPI.Services.DomainService.Implementation
{
    public class ProductService : IProductService
    {
        private readonly ProductDBContext _dbContext;

        public ProductService(ProductDBContext productDbContext)
        {
            _dbContext = productDbContext;
        }

        public async Task<IEnumerable<ProductModel>> GetModelList()
        {

            var entity = await _dbContext.Product.ToListAsync();

            return entity.Select(a => new ProductModel
            {
                Id = a.Id,
                Name = a.Name,
                Code = a.Code,
                Price = a.Price
            });
        }

        public async Task<ProductModel> GetById(int productId)
        {
            ProductModel model = new ProductModel();
            var entity = await _dbContext.Product.FindAsync(productId);

            if (entity != null)
            {
                TranformEntity(entity, model);
            }

            return model;
        }

        public async Task<SaveResult> Create(NewProductModel model)
        {
            SaveResult saveResult = new SaveResult();
            try
            {
                Product entity = new Product();

                TranformModel(model, entity);

                await _dbContext.Product.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                saveResult.IsSuccess = true;
            }
            catch (Exception)
            {
                saveResult.Message = "Error Saving Record";
            }

            return saveResult;
        }


        public async Task<SaveResult> Update(ProductModel model)
        {
            SaveResult saveResult = new SaveResult();
            try
            {
                Product entity = await _dbContext.Product.FindAsync(model.Id);
                if (entity != null)
                {
                    TranformModel(model, entity);
                    _dbContext.Product.Update(entity);
                    await _dbContext.SaveChangesAsync();
                    saveResult.IsSuccess = true;
                }
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
                var entity = await _dbContext.Product.FindAsync(productId);
                if (entity != null)
                {
                    _dbContext.Product.Remove(entity);
                    await _dbContext.SaveChangesAsync();
                    saveResult.IsSuccess = true;
                }
            }
            catch (Exception)
            {
                saveResult.Message = "Error Deleting Record";
            }

            return saveResult;
        }

        #region Transform

        private static void TranformModel(ProductModel model, Product entity)
        {
            entity.Name = model.Name;
            entity.Code = model.Code;
            entity.Price = model.Price;
            entity.UpdatedTimestamp = DateTime.Now;
            //entity.UserId = model.UserId;
        }

        private static void TranformModel(NewProductModel model, Product entity)
        {
            entity.Name = model.Name;
            entity.Code = model.Code;
            entity.Price = model.Price;
            //entity.UserId = model.UserId;
        }

        private static void TranformEntity(Product entity, ProductModel model)
        {
            model.Id = entity.Id;
            model.Name = entity.Name;
            model.Code = entity.Code;
            model.Price = entity.Price;
            //model.UserId = entity.UserId;


        }

        #endregion
    }
}
