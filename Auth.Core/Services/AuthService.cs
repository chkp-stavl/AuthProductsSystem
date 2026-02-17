using Auth.Core.DTOs;
using Auth.Core.Interfaces;

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
            if (user == null || !_hasher.Verify(user.PasswordHash, password))
            {
                return LoginResponse.Fail("Invalid credentials");
            }

            user.LastLogin = DateTime.UtcNow;
            await _users.UpdateUserLastLogInAsync(user);

            var token = _tokens.CreateToken(user.Id, user.UserName, user.Role);

            return LoginResponse.Success(
                user.Id,
                user.UserName,
                user.Role == Enums.UserRole.Admin,
                token);
        }
    }
}
