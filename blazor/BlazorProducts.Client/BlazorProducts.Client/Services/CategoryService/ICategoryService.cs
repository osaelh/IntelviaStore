using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorProducts.Client.Services.CategoryService
{
    public interface ICategoryService
    {
        List<Category> Categories { get; set; }

        void LoadCategories();
        Task LoadCategoriesAsync();

        Task CreateCategory(Category category);

        Task DeleteCategory(string categoryUrl);

        Task<Category> GetCategory(string categoryUrl);

        Task UpdateCategory(Category category);
    }
}
