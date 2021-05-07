using Blazored.LocalStorage;
using Blazored.Toast.Services;
using BlazorProducts.Client.Services.ProductService;
using Entities.DTO;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorProducts.Client.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IToastService _toastService;
        private readonly IProductService _productService;

        public event Action OnChange;

        public CartService(ILocalStorageService localStorageService, IToastService toastService, IProductService productService)
        {
            _localStorageService = localStorageService;
            _toastService = toastService;
            _productService = productService;
        }

        public async Task AddToCart(ProductVariant productVariant)
        {
            var cart = await _localStorageService.GetItemAsync<List<ProductVariant>>("cart");
            if (cart == null)
            {
                cart = new List<ProductVariant>();
            }
            cart.Add(productVariant);
            await _localStorageService.SetItemAsync("cart", cart);
            var product = await _productService.GetProductById(productVariant.ProductId);
            _toastService.ShowSuccess(product.Title, "Added to cart");

            OnChange.Invoke();
        }

        public async Task<List<CartItem>> GetCartItems()
        {
            var result = new List<CartItem>();
            var cart = await _localStorageService.GetItemAsync<List<ProductVariant>>("cart");
            if (cart == null)
            {
                return result;
            }

            foreach (var item in cart)
            {
                var product = await _productService.GetProductById(item.ProductId);

                var cartItem = new CartItem
                {
                    ProductId = product.Id,
                    ProductTitle = product.Title,
                    EditionId = item.EditionId,
                    Image = product.Image
                };

                var variant = product.Variants.Find(v => v.EditionId == item.EditionId);
                if (variant != null)
                {
                    cartItem.EditionName = variant.Edition?.Name;
                    cartItem.Price = variant.Price;
                }
                result.Add(cartItem);
            }
            return result;
        }
    }
}
