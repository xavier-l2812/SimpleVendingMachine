using SimpleVendingMachine.Models.Dtos;
using SimpleVendingMachine.Web.Services.Contracts;
using System.Net.Http.Json;

namespace SimpleVendingMachine.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient httpClient;

        public ProductService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public Task<ProductDto> GetItem(int productId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductDto>> GetItems()
        {
            try
            {
                var response = await httpClient.GetAsync("api/Product");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ProductDto>();
                    }

                    return await response.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status code: {response.StatusCode} message: {message}");
                }
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        public Task<IEnumerable<ProductDto>> GetItemsByCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductCategoryDto>> GetProductCategories()
        {
            throw new NotImplementedException();
        }
    }
}
