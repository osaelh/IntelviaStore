using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorProducts.Server.Repository
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProduct(int id);
        Task<List<Product>> GetProductsByCategory(string categoryUrl);

        Task CreateProduct(Product product);
    }
}