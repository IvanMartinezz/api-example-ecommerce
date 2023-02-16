using DemoAPI.Helpers;
using DemoAPI.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace DemoAPI.Repository
{
    public class RoleRepository : IRoleRepository
    {
        protected readonly DataContext _context;

        public RoleRepository(DataContext context) => _context = context;
        
        public async Task<Role> Create(Role role)
        {
            try
            {
                if (_context.Roles.Any(x => x.Name == role.Name))
                    throw new Exception($"The rol { role.Name } already exists");
                await _context.AddAsync(role);
                await _context.SaveChangesAsync();
                return role;
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Role> GetRoles()
        {
            try
            {
                return _context.Roles.ToList();
            } catch (Exception ex)
            {
                throw ex;
            }
        }
        public Role GetByName(string name)
        {
            try
            {
                var role = _context.Roles.FirstOrDefault(m => m.Name == name);
                return role;
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Update(Role role)
        {
            try
            {
                _context.Entry(role).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
