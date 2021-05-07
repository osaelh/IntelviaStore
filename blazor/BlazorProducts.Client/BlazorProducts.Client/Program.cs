using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using BlazorProducts.Client.HttpRepository;
using Tewr.Blazor.FileReader;
using Microsoft.AspNetCore.Components.Authorization;
using BlazorProducts.Client.AuthProviders;
using Blazored.LocalStorage;
using BlazorProducts.Client.Services.ProductService;
using BlazorProducts.Client.Services.CategoryService;
using Blazored.Toast;
using BlazorProducts.Client.Services.CartService;

namespace BlazorProducts.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
/*            builder.Services.AddScoped<IProductHttpRepository, ProductHttpRepository>();*/
            builder.Services.AddFileReaderService(o => o.UseWasmSharedBuffer = true);
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<ICartService, CartService>();

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddBlazoredToast();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

            await builder.Build().RunAsync();
        }
    }
}
