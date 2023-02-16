using DemoAPI.Models.Data;
using DemoAPI.Models.DTOs;
using DemoAPI.Models.Repository;
using DemoAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository _productRepository;
        private ICategoryRepository _categoryRepository;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
     
        //[Authorize(Policy = "RequireAdministratorRole")]
        [HttpGet]
        [ActionName(nameof(GetProductsAsync))]
        public IEnumerable<Product> GetProductsAsync(int lastId)
        {
            try
            {
                return _productRepository.GetProducts(lastId);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{id}")]
        [ActionName(nameof(GetProductById))]
        public ActionResult<Product> GetProductById(int id)
        {
            try
            {
                var product = _productRepository.GetProductById(id);
                if (product == null) return NotFound();
                return product;
            } catch(Exception ex )
            {
                throw ex;
            }
        }

        //[Authorize]
        [HttpPost]
        [ActionName(nameof(CreateProductAsync))]
        public async Task<IResult> CreateProductAsync(ProductDTO product)
        {
            try
            {
                if (product == null) return Results.BadRequest();
                var Category = _categoryRepository.GetByName(product.Category);
                if (Category == null) return Results.NotFound("The category is wrong");

                var ProductMap = new Product();
                ProductMap.Name = product.Name;
                ProductMap.Description = product.Description;
                ProductMap.Price = product.Price;
                ProductMap.Category_id = Category.Id;
                ProductMap.Created_at = DateTime.Now;
                ProductMap.Updated_at = DateTime.Now;

                var createdProduct = await _productRepository.CreateProductAsync(ProductMap);
                return Results.Ok(createdProduct);
            } catch(Exception ex)
            {
                throw ex;
            }
        }

        //[Authorize]
        [HttpPut("{id}")]
        [ActionName(nameof(UpdateProduct))]
        public async Task<ActionResult> UpdateProduct(int id, ProductDTO product)
        {
            try
            {
                if (product == null) return BadRequest();

                var productDB = _productRepository.GetProductById(id);

                productDB.Name = product.Name;
                productDB.Description = product.Description;
                productDB.Price = product.Price;
                productDB.Updated_at = DateTime.Now;
                
                var categoryDB = _categoryRepository.GetByName(product.Category);

                if(productDB.Category_id != categoryDB.Id) productDB.Category_id = categoryDB.Id;
             
                await _productRepository.UpdateProductAsync(productDB);

                return Ok("Updated product");
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        [ActionName(nameof(DeleteProduct))]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = _productRepository.GetProductById(id);
                if (product == null) return NotFound();

                await _productRepository.DeleteProductAsync(product);
                return Ok("Deleted product");
            } catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}