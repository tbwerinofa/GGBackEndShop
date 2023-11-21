using Microsoft.EntityFrameworkCore;
using ProductWebAPI.DataRepository;
using ProductWebAPI.Models;

namespace ProductWebAPI.Services.DomainService.Implementation
{

    public class ProductImageService : IProductImageService
    {
        #region global fields
        private readonly ProductDBContext _dbContext;
        #endregion

        #region ctor
        public ProductImageService(ProductDBContext productDbContext)
        {
            _dbContext = productDbContext;
        }

        #endregion

        #region Methods
        public async Task<ProductImageModel> GetById(int Id,string userId)
        {
            ProductImageModel model = new ProductImageModel();

            var entity = await _dbContext.ProductImage.Include(a=>a.Product).FirstOrDefaultAsync(a=>a.Product.UserId == userId && a.Id == Id);

            if (entity != null)
            {
                TranfromModel(model, entity);
            }


            return model;
        }

        public async Task<SaveResult> Save(ProductImageModel model)
        {
            SaveResult saveResult = new SaveResult();
            try
            {
                ProductImage entity = new ProductImage();

                TranformEntity(entity, model);

                await _dbContext.ProductImage.AddAsync(entity);
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
                var entity = await _dbContext.ProductImage.FindAsync(productId);
                if (entity != null)
                {
                    _dbContext.ProductImage.Remove(entity);
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
        #endregion

        #region Transform
        private static void TranfromModel(ProductImageModel model, ProductImage entity)
        {
            model.ProductId = entity.ProductId;
            model.FileNameGuid = entity.FileNameGuid;
            model.FileName = entity.FileName;
        }

        private static void TranformEntity(ProductImage entity, ProductImageModel model)
        {
            entity.ProductId = model.ProductId;
            entity.FileNameGuid = model.FileNameGuid;
            entity.FileName = model.FileName;
        }

        #endregion

    }
}
