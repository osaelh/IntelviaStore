using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorProducts.Client.Services.ProductService
{
    public interface IProductService
    {
        event Action OnChange;
        List<Product> Products { get; set; }
         Task LoadProductsAsync(string categoryUrl = null);
        Task<Product> GetProductById(int id);
        Task CreateProduct(Product product);
    }
}
