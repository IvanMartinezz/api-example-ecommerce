using DemoAPI.Models.Data;
using DemoAPI.Models.Repository;
using DemoAPI.Models.Users;
using DemoAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUser _userService;
        private IRoleRepository _rolRepository;

        public UserController(IUser userService, IRoleRepository rolRepository)
        {
            _userService = userService;
            _rolRepository = rolRepository;
        }

        [HttpPost("login")]
        [ActionName(nameof(Login))]
        public ActionResult<AuthenticateResponse> Login(AuthenticateRequest user)
        {
            try
            {
                if (user == null) return NotFound();
                var response = _userService.Login(user);
                return Ok(response);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("register")]
        [ActionName(nameof(RegisterAsync))]
        public async Task<IResult> RegisterAsync(RegisterRequest user)
        {
            try
            {
                if (user == null) return Results.BadRequest();

                var Role = _rolRepository.GetByName(user.Role);
                if (Role == null) return Results.NotFound("The role is wrong");
                
                var UserMap = new User();
                UserMap.Name = user.Name;
                UserMap.Email = user.Email;
                UserMap.Password = user.Password;
                UserMap.Role_id = Role.Id;
                UserMap.Created_at = DateTime.Now;
                UserMap.Updated_at = DateTime.Now;

                var createdUser = await _userService.RegisterAsync(UserMap);
                return Results.Ok(createdUser);
            } catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("changePassword")]
        [ActionName(nameof(UpdatePasswordById))]
        public async Task<ActionResult> UpdatePasswordById(int id, ChangePasswordRequest changePasswordRequest)
        {
            try
            {

                // PONER CONDICION PARA CUANDO LA PASS SEA IGUAL A LA ANTIGUA
                if (changePasswordRequest == null) return BadRequest();

                var userMap = new ChangePasswordResponse();
                userMap.Id = id;
                userMap.Password = changePasswordRequest.Password;
                userMap.NewPassword = changePasswordRequest.NewPassword;
                await _userService.UpdatePassword(userMap);

                return Ok("Updated password");
            } catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("changeRole")]
        [ActionName(nameof(UpdateRoleByUserIdAndRole))]
        public async Task<ActionResult> UpdateRoleByUserIdAndRole(int id, string role)
        {
            try
            {
                if (role == null) return BadRequest();

                var Role = _rolRepository.GetByName(role);
                var user = _userService.GetById(id);
                if (Role == null || user.Role_id == Role.Id) return BadRequest("The role entered does not exist or is the one you already have");

                user.Role_id = Role.Id;

                await _userService.UpdateRole(user);

                return Ok("Updated role");

            } catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
