using Auth.Core.Common;
using Auth.Core.DTOs;
using Auth.Core.Entities;
using Auth.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Core.Services
{
    public class AuthService
    {
        private readonly IUserRepository _users;
        private readonly IPasswordHasher _hasher;
        private readonly ITokenService _tokens;


        public AuthService(IUserRepository users, IPasswordHasher hasher, ITokenService tokens)
        {
            _users = users;
            _hasher = hasher;
            _tokens = tokens;
        }

       

        public async Task<LoginResponse> Login(string userName, string password)
        {
            var user = await _users.GetByUserNameAsync(userName);
            if (user == null)
            {
                return LoginResponse.Fail("Invalid credentials");
            }
               

            if (!_hasher.Verify(user.PasswordHash, password))
            {
                return LoginResponse.Fail("Invalid credentials");
            }

            user.LastLogin = DateTime.UtcNow;
            

            var token = _tokens.CreateToken(user.Id, user.UserName, user.Role);
            return LoginResponse.Success(
             user.Id,
             user.UserName,
             (user.Role == Enums.UserRole.Admin) ? true : false,
             token);
        }

       /* private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }*/


        /*public async Task<Result<UserDto>> GetCurrentUser(Guid userId)
        {
            var user = await _users.GetByIdAsync(userId);
            if (user == null)
            {
                return Result<UserDto>.Fail("User not found");
            }
                

            return Result<UserDto>.Ok(
                new UserDto(
                    user.Id,
                    user.Email,
                    user.FirstName,
                    user.LastName,
                    user.CreatedAt));
        }*/

    }
}
