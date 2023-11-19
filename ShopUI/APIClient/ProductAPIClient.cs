using Microsoft.Extensions.Caching.Memory;
using ShopUI.Models;
using System.Net;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

namespace ShopUI.APIClient
{
    public class ProductAPIClient : IProductAPIClient
    {
        private readonly HttpClient _client;
        private readonly IMemoryCache _memoryCache;
        private const string RequestUri = "product";
        public ProductAPIClient(HttpClient client,
            IConfiguration config,
            IMemoryCache memoryCache)
        {
            _client = client;
            _client.BaseAddress = new System.Uri(config.GetValue<string>("ProductApi"));
            _memoryCache = memoryCache;
        }

        #region Read

        public async Task<IEnumerable<ProductModel>> GetModelList(string token)
        {
           
            try
            {

               // _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _client.GetFromJsonAsync<List<ProductModel>>(RequestUri);


            return response;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<ProductModel> GetModelById(int Id)
        {

            try
            {
                var route =$"{RequestUri}/{Id}";
                // _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _client.GetFromJsonAsync<ProductModel>(route);


                return response;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion

        #region Save/Update

        public async Task<bool> Create(ProductModel model)
        {
            try
            {
                var response = await _client.PostAsync(RequestUri, model, new JsonMediaTypeFormatter());
                bool saveResult = await response.Content.ReadAsAsync<bool>();
                return saveResult;
            }
            catch (Exception ex)
            {

                throw;
            }
         
            
            
        }

        public async Task<bool> Update(ProductModel model)
        {

            var response = await _client.PutAsync(RequestUri, model, new JsonMediaTypeFormatter());
            bool saveResult = await response.Content.ReadAsAsync<bool>();

            return saveResult;
        }
        #endregion

        #region Delete
        public async Task<bool> DeleteById(int Id)
        {
            var route = $"{RequestUri}/{Id}";
            var response = await _client.DeleteAsync(route);
            return response.IsSuccessStatusCode;

        }

        #endregion
    }
}
