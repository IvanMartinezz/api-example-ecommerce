using DemoAPI.Models.Data;
using DemoAPI.Models.DTOs;
using DemoAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository) => _categoryRepository = categoryRepository;

        [HttpGet("list")]
        [ActionName(nameof(GetCategories))]
        public IEnumerable<Category> GetCategories()
        {
            try
            {
                return _categoryRepository.GetCategories();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("byName")]
        [ActionName(nameof(GetByName))]
        public ActionResult<Category> GetByName(string name)
        {
            try
            {
                if(name == null) return NotFound();
                var category = _categoryRepository.GetByName(name);
                if(category == null) return NotFound("The category does not exist");
                return category;
            } catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [ActionName(nameof(CreateCategory))]
        public async Task<IResult> CreateCategory(CategoryDTO category)
        {
            try
            {
                if (category == null) return Results.NotFound();
                var Category = new Category();
                Category.Name = category.Name;
                var createdCategory = await _categoryRepository.Create(Category);
                return Results.Ok(createdCategory);
            } catch(Exception ex)
            {
                throw ex;
            }
        }


        [HttpPut("{id}")]
        [ActionName(nameof(UpdatedCategory))]
        public async Task<ActionResult> UpdatedCategory(int id, CategoryDTO category)
        {
            try
            {
                var CategoryDB = _categoryRepository.GetById(id);

                if(CategoryDB.Name == category.Name) return NotFound("The entered category is equal to the existing one");

                CategoryDB.Name = category.Name;
                await _categoryRepository.Update(CategoryDB);
                return Ok("Updated category");
            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
