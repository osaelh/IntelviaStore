
using BlazorProducts.Server.Repository;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorProducts.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategorySerice _categoryService;

        public CategoryController(ICategorySerice categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetCategories()
        {
            return Ok(await _categoryService.GetCategories());
        }
        
        [HttpGet("{categoryurl}")]
        public async Task<ActionResult<Category>> GetCategoryByUrl(string categoryUrl)
        {
           return Ok(await _categoryService.GetCategoryByUrl(categoryUrl));
            
        }


        [HttpPost]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            if (category == null || ModelState.IsValid == false)
            {
                return BadRequest();
            }
            await _categoryService.CreateCategory(category);
            return Created("", category);
        }

        [HttpPut("{categoryurl}")]
        public async Task<IActionResult> UpdateCategory(string categoryUrl, [FromBody] Category category)
        {
            //additional product and model validation checks

            var dbCategory = await _categoryService.GetCategoryByUrl(categoryUrl);
            if (dbCategory == null)
                return NotFound();

            await _categoryService.UpdateCategory(category, dbCategory);

            return NoContent();
        }

        [HttpDelete("{categoryurl}")]
        public async Task<IActionResult> DeleteCategory(string categoryUrl)
        {
            var category = await _categoryService.GetCategoryByUrl(categoryUrl);
            if (category == null)
                return NotFound();

            await _categoryService.DeleteCategory(category);

            return NoContent();
        }
    }
}
