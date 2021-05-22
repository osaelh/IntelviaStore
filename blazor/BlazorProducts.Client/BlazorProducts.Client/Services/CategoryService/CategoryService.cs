using Blazored.Toast.Services;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.IO;
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

        public async Task UpdateCategory(Category category)
        {
            var content = JsonSerializer.Serialize(category);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var url = Path.Combine("https://localhost:5011/api/category/", category.Url);

            var putResult = await _httpClient.PutAsync(url, bodyContent);
            var putContent = await putResult.Content.ReadAsStringAsync();
            _toastService.ShowInfo("Category", "Updated Succuessfully");
            await Task.Delay(2000);
            if (!putResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(putContent);
            }
        }

        public async Task DeleteCategory(string categoryUrl)
        {
            var url = Path.Combine("https://localhost:5011/api/category", categoryUrl);

            var deleteResult = await _httpClient.DeleteAsync(url);
            var deleteContent = await deleteResult.Content.ReadAsStringAsync();
            _toastService.ShowInfo("Category", "Deleted Succuessfully");
            await Task.Delay(2000);
            if (!deleteResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(deleteContent);
            }
        }

        public async Task<Category> GetCategory(string categoryUrl)
        {
            var url = Path.Combine("https://localhost:5011/api/category/", categoryUrl);

            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var category = JsonSerializer.Deserialize<Category>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return category;
        }

    }
}
