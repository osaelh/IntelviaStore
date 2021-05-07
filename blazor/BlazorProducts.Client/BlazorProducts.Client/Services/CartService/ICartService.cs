using Entities.DTO;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorProducts.Client.Services.CartService
{
    public interface ICartService
    {
        event Action OnChange;
        Task AddToCart(ProductVariant productVariant);
        Task<List<CartItem>> GetCartItems();
    }
}
