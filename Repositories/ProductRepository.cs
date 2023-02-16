using DemoAPI.Helpers;
using DemoAPI.Models.Data;
using DemoAPI.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DemoAPI.Models.Repository
{
    public class ProductRepository : IProductRepository
    {
        protected readonly DataContext _context;
        public ProductRepository(DataContext context) => _context = context;

        public async Task<Product> CreateProductAsync(Product product)
        {
            try
            {
                if (_context.Products.Any(x => x.Name == product.Name))
                    throw new Exception($"The product { product.Name } already exists");
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                return product;
            } catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteProductAsync(Product product)
        {
            try
            {
                if (product is null) return false;
                _context.Set<Product>().Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Product> GetProducts(int lastId)
        {
            try
            {
                var nextPage = _context.Products
                    .OrderBy(b => b.Id)
                    .Where(b => b.Id > lastId)
                    .Take(10)
                    .ToList();
                return nextPage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Product GetProductById(int id)
        {
            try
            {
                return _context.Products.Find(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            try
            {
                _context.Entry(product).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            } catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}