using BlazorProducts.Server.Context;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorProducts.Server.Repository
{
    public class CategorySerice : ICategorySerice
    {
        private readonly ProductContext _context;

        public CategorySerice(ProductContext context)
        {
            _context = context;
        }

        public async Task CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategory(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategory(Category category, Category dbCategory)
        {
            dbCategory.Name = category.Name;
            dbCategory.Url = category.Url;
            dbCategory.Icon = category.Icon;
            await _context.SaveChangesAsync();
        }
        public async Task<List<Category>> GetCategories()
        {
             return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByUrl(string categoryUrl)
        {
           return await _context.Categories.
            FirstOrDefaultAsync(c => c.Url.ToLower().Equals(categoryUrl.ToLower()));
        }
    }
}
