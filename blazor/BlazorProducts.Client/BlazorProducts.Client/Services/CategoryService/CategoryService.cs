using Blazored.Toast.Services;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorProducts.Client.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;
        private readonly IToastService _toastService;

        public List<Category> Categories { get; set; } = new List<Category>();

        public CategoryService(HttpClient httpClient, IToastService toastService)
        {
            _httpClient = httpClient;
            _toastService = toastService;
        }


        public async Task LoadCategoriesAsync()
        {
            Categories = await _httpClient.GetFromJsonAsync<List<Category>>("https://localhost:5011/api/category");
        }

        public void LoadCategories()
        {
            throw new NotImplementedException();
        }

        public async Task CreateCategory(Category category)
        {
            var content = JsonSerializer.Serialize(category);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var postResult = await _httpClient.PostAsync("https://localhost:5011/api/category", bodyContent);
            var postContent = await postResult.Content.ReadAsStringAsync();
            _toastService.ShowSuccess(category.Name, "Added");

            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
        }

        public Task DeleteProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
