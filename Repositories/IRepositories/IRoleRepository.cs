using DemoAPI.Models.Data;

namespace DemoAPI.Repository
{
    public interface IRoleRepository
    {
        Task<Role> Create(Role role);
        // Task<bool> Detele(Role role);
        IEnumerable<Role> GetRoles();
        Role GetByName(string name);
        Task<bool> Update(Role role);
    }
}
