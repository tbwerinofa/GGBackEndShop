using Microsoft.EntityFrameworkCore;
using ProductWebAPI.DataRepository;
using ProductWebAPI.Models;

namespace ProductWebAPI.Services.DomainService.Implementation
{
    public class ProductService : IProductService
    {

        #region global fields
        private readonly ProductDBContext _dbContext;
        #endregion


        #region CTOR
        public ProductService(ProductDBContext productDbContext)
        {
            _dbContext = productDbContext;
        }
        #endregion

        #region CRUD
        public async Task<IEnumerable<ProductModel>> GetModelList(string userId)
        {

            var entity = await _dbContext.Product
                .Where(a=>a.UserId == userId)
                .ToListAsync();

            return entity.Select(a => new ProductModel
            {
                Id = a.Id,
                Name = a.Name,
                Code = a.Code,
                Price = a.Price
            });
        }

        public async Task<ProductModel> GetById(int productId, string userId)
        {
            ProductModel model = new ProductModel();
            var entity = await _dbContext.Product.FirstOrDefaultAsync(a => a.Id == productId && a.UserId == userId);

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
                Product entity = TransformModel(model);

                await _dbContext.Product.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                saveResult.IsSuccess = true;
            }
            catch (DbUpdateException upDateEx)
            {
                var results = upDateEx.GetSqlerrorNo();
                if (results == (int)SqlErrNo.UQ)
                {
                    saveResult.Message = ConstEntity.UniqueKeyMsg;
                }
                else
                {
                    saveResult.Message = "Error Saving Record";
                }
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
                var entity = await _dbContext.Product.FirstOrDefaultAsync(a=>a.Id == model.Id && a.UserId == model.UserId);

                if (entity != null)
                {
                    TranformModel(model, entity);
                    _dbContext.Product.Update(entity);
                    await _dbContext.SaveChangesAsync();
                    saveResult.IsSuccess = true;
                }
            }
            catch (DbUpdateException upDateEx)
            {
                var results = upDateEx.GetSqlerrorNo();
                if(results == (int)SqlErrNo.UQ)
                {
                 saveResult.Message = ConstEntity.UniqueKeyMsg;
                }else
                {
                    saveResult.Message = "Error Saving Record";
                }
            }
            catch (Exception)
            {
                saveResult.Message = "Error Saving Record";
            }

            return saveResult;
        }


        public async Task<SaveResult> Delete(int productId,string userId)
        {
            SaveResult saveResult = new SaveResult();
            try
            {
                var entity = await _dbContext.Product.Include(a=>a.ProductImages).FirstOrDefaultAsync(a => a.Id == productId && a.UserId == userId);


                if (entity != null)
                {
                    if (!entity.ProductImages.Any())
                    {
                        _dbContext.Product.Remove(entity);
                        await _dbContext.SaveChangesAsync();
                        saveResult.IsSuccess = true;
                    }
                    else
                    {
                        saveResult.Message = "Error Deleting Record: Product has related images";
                    }

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

        private static void TranformModel(ProductModel model, Product entity)
        {
            entity.Name = model.Name;
            entity.Code = model.Code;
            entity.Price = model.Price;
            entity.UpdatedTimestamp = DateTime.Now;
            //entity.UserId = model.UserId;
        }

        private static Product TransformModel(NewProductModel model)
        {
            return new Product
            { 
                Name = model.Name,
                Code = model.Code,
                Price = model.Price,
                UserId = model.UserId
            };
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
