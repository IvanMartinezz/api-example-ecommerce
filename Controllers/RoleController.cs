using DemoAPI.Models.Data;
using DemoAPI.Models.DTOs;
using DemoAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private IRoleRepository _roleRepository;

        public RoleController(IRoleRepository roleRepository) => _roleRepository = roleRepository;

        [HttpGet("list")]
        [ActionName(nameof(GetRoles))]
        public IEnumerable<Role> GetRoles()
        {
            try
            {
                return _roleRepository.GetRoles();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [ActionName(nameof(GetByName))]
        public ActionResult<Role> GetByName(string name)
        { try
            {
                var role = _roleRepository.GetByName(name);
                if (role == null) return NotFound("The role is wrong");
                return role;
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [ActionName(nameof(CreateRole))]
        public async Task<IResult> CreateRole(RoleDTO role)
        {
            try
            {
                if (role == null) return Results.NotFound();

                var Role = new Role();
                Role.Name = role.Name;

                var createdRole = await _roleRepository.Create(Role);
                return Results.Ok(createdRole);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("{id}")]
        [ActionName(nameof(UpdatedRole))]
        public async Task<ActionResult> UpdatedRole(int id, RoleDTO role)
        {
            try
            {
                var updatedRole = new Role();
                updatedRole.Id = id;
                updatedRole.Name = role.Name;
                await _roleRepository.Update(updatedRole);
            
                return Ok("Updated rol");
            } catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
