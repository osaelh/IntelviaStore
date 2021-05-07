using BlazorProducts.Server.Context;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorProducts.Server.Repository
{
    public class ProductService : IProductService
    {
        private readonly ICategorySerice _categoryService;
        private readonly ProductContext _context;

        public ProductService(ICategorySerice categoryService, ProductContext context)
        {
            _categoryService = categoryService;
            _context = context;
        }

        public async Task CreateProduct(Product product)
        {
            _context.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products
                .Include(p=>p.Variants)
                .ToListAsync();
        }

        public async Task<Product> GetProduct(int id)
        {
            Product product = await _context.Products
                .Include(p=>p.Variants)
                .ThenInclude(v=> v.Edition)
                .FirstOrDefaultAsync(p => p.Id == id);
            return  product;
        }


        public async Task<List<Product>> GetProductsByCategory(string categoryUrl)
        {
              Category category = await _categoryService.GetCategoryByUrl(categoryUrl);
              return await _context.Products.Include(p=>p.Variants).Where(p => p.CategoryId == category.Id).ToListAsync();
          
        }
    }
}
