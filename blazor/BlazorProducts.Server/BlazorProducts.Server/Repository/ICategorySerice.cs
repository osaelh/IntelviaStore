using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorProducts.Server.Repository
{
    public interface ICategorySerice
    {
        Task<List<Category>> GetCategories();
        Task<Category> GetCategoryByUrl(string categoryUrl);
        Task CreateCategory(Category category);
        Task DeleteCategory(Category category);
        Task UpdateCategory(Category category, Category dbCategory);
    }
}