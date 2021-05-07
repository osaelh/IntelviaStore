using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;

namespace BlazorProducts.Client.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public event Action OnChange;

        public List<Product> Products { get; set; } = new List<Product>();

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task LoadProductsAsync(string categoryUrl = null)
        {

            if (categoryUrl == null)
            {
                Products = await _httpClient.GetFromJsonAsync<List<Product>>("https://localhost:5011/api/Product");

            }
            else
            {
                Products = await _httpClient.GetFromJsonAsync<List<Product>>($"https://localhost:5011/api/Product/Category/{categoryUrl}");

            }

            OnChange.Invoke();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _httpClient.GetFromJsonAsync<Product>($"https://localhost:5011/api/Product/{id}");
        }

        public async Task CreateProduct(Product product)
        {
            var content = JsonSerializer.Serialize(product);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var postResult = await _httpClient.PostAsync("https://localhost:5011/api/products", bodyContent);
            var postContent = await postResult.Content.ReadAsStringAsync();

            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
        }
    }
}
