using goods_server.Contracts;
using goods_server.Service.InterfaceService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace goods_server.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDTO createCategoryDTO)
        {
            try
            {
                var result = await _categoryService.CreateCategoryAsync(createCategoryDTO);
                if (result)
                {
                    return Ok("Category created successfully.");
                }
                return BadRequest("Failed to create category.");
            }
            catch (ArgumentException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] UpdateCategoryDTO updateCategoryDTO)
        {
            try
            {
                var result = await _categoryService.UpdateCategoryAsync(id, updateCategoryDTO);
                if (result)
                {
                    return Ok("Category updated successfully.");
                }
                return BadRequest("Failed to update category.");
            }
            catch (ArgumentException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var (success, message) = await _categoryService.DeleteCategoryAsync(id);
            if (success)
            {
                return Ok(message);
            }
            return BadRequest(message);
        }
    }
}
