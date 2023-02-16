using DemoAPI.Models.Data;
using DemoAPI.Models.Users;

namespace DemoAPI.Models.Repository
{
    public interface IUser
    {
        Token Login(AuthenticateRequest userCredentials);
        User GetById(int id);
        Task<User> RegisterAsync(User user);
        Task<bool> UpdatePassword(ChangePasswordResponse user);
        Task<bool> UpdateRole(User user);
    }
}
