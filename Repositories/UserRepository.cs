using DemoAPI.Helpers;
using DemoAPI.Models.Users;
using DemoAPI.Authorization;
using DemoAPI.Models.Repository;
using DemoAPI.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace DemoAPI.Services
{
    public class UserRepository : IUser
    {
        protected readonly DataContext _context;
        private IJwtUtils _jwtUtils;

        public UserRepository(DataContext context, IJwtUtils jwtUtils)
        {
            _context = context;
            _jwtUtils = jwtUtils;
        }

        public Token Login(AuthenticateRequest userCredentials)
        {
            try
            {
                var userResponse = _context.Users.FirstOrDefault(m => m.Email == userCredentials.Email);
                if (userResponse == null) throw new Exception("Email is incorrect");

                if (!BCrypt.Net.BCrypt.Verify(userCredentials.Password, userResponse?.Password))
                    throw new Exception("Username or password is incorrect");
               
                var response = _jwtUtils.GenerateToken(userResponse);
                return response;

            } catch (Exception ex)
            {
                throw ex;
            }
        }
        public User GetById(int id)
        {
            try
            {
                return _context.Users.Find(id);

            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User> RegisterAsync(User user)
        {
            try
            {
                if (_context.Users.Any(x => x.Email == user.Email))
                    throw new Exception($"Email {user.Email} is already taken");

                var passEncrypt = BCrypt.Net.BCrypt.HashPassword(user.Password);
                user.Password = passEncrypt;
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return user;

            } catch (Exception ex)
            {
                throw ex;
            }
        }

        // FALTA VALIDACION PARA QUE SI O SI TE HAGA CAMBIAR LA PASS, TE DEJA INGRESAR LA MISMA
        public async Task<bool> UpdatePassword(ChangePasswordResponse user)
        {
            try
            {
                var userResponse = _context.Users.FirstOrDefault(m => m.Id == user.Id);

                if (userResponse.Password == null || !BCrypt.Net.BCrypt.Verify(user.Password, userResponse.Password))
                    throw new Exception("Password is incorrect");

                var newPassword = BCrypt.Net.BCrypt.HashPassword(user.NewPassword);

                // NO FUNCIONA LA VALIDACION 
                if (userResponse.Password == newPassword) 
                    throw new Exception("The password must be different");

                userResponse.Password = newPassword;
                _context.Entry(userResponse).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateRole(User user)
        {
            try
            {
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
