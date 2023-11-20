using ProductWebAPI.DataRepository;
using ProductWebAPI.Models;

namespace ProductWebAPI.Services.DataRepository.Service
{

    public class ProductImageService: IProductImageService
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
        public async Task<ProductImageModel> GetById(int productId)
        {
            ProductImageModel model = new ProductImageModel();

            var entity = await _dbContext.ProductImage.FindAsync(productId);

            if(entity != null)
            {
                TranfromModel(model,entity);
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
                if(entity !=null)
                {
                    _dbContext.ProductImage.Remove(entity);
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

        private static void TranfromModel(ProductImageModel model, ProductImage entity)
        {
            model.ProductId = entity.ProductId;
            model.FileNameGuid = entity.FileNameGuid;
            model.FileName = entity.FileName;
            model.UserId = entity.UserId;
        }

        private static void TranformEntity(ProductImage entity,ProductImageModel model)
        {
            entity.ProductId = model.ProductId;
            entity.FileNameGuid = model.FileNameGuid;
            entity.FileName = model.FileName;
            entity.UserId = model.UserId;
        }

        #endregion

    }
}
