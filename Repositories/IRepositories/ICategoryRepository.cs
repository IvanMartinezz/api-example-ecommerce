using DemoAPI.Models.Data;

namespace DemoAPI.Repository
{
    public interface ICategoryRepository
    {
        Task<Category> Create(Category category);
        Category GetById(int id);
        Category GetByName (string name);
        IEnumerable<Category> GetCategories();
        Task<bool> Update(Category category);
    }
}
