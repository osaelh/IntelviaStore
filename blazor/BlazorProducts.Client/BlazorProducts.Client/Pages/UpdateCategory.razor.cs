using BlazorProducts.Client.Services.CategoryService;
using Entities.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorProducts.Client.Pages
{
    public partial class UpdateCategory
    {
        private Category _category ;
     

        [Inject]
        ICategoryService categoryService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string CategoryUrl { get; set; }

        protected async override Task OnInitializedAsync()
        {
            _category = await categoryService.GetCategory(CategoryUrl);
        }

        private async Task Update()
        {
            await categoryService.UpdateCategory(_category);
            NavigationManager.NavigateTo("/categories", true);


        }
    }
}
