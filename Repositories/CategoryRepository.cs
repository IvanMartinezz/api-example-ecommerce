using DemoAPI.Helpers;
using DemoAPI.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace DemoAPI.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        protected readonly DataContext _context;

        public CategoryRepository(DataContext context) => _context = context;

        public async Task<Category> Create(Category category)
        {
            try
            {
                if (_context.Categories.Any(x => x.Name == category.Name))
                    throw new Exception($"The category { category.Name } already exists");
                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();
                return category;
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Category> GetCategories()
        {
            try
            {
                return _context.Categories.ToList();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public Category GetById(int id)
        {
            try
            {
                return _context.Categories.FirstOrDefault();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public Category GetByName(string name)
        {
            try
            {
                return _context.Categories.FirstOrDefault(m => m.Name == name);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Update(Category category)
        {
            try
            {
                _context.Entry(category).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            } catch (Exception ex)
            {
                throw ex;
            }
             
        }
    }
}
