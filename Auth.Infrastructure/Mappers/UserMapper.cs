using Auth.Core.Entities;
using Auth.Core.Enums;
using Auth.Infrastructure.Data;

namespace Auth.Infrastructure.Mappers
{
    public static class UserMapper
    {
        public static AppUser ToEntity(User user)
        {
            return new AppUser
            {
                Id = user.Id,
                UserName = user.UserName,
                PasswordHash = user.PasswordHash,
                Role = (int)user.Role,
                CreatedAt = user.CreatedAt,
                LastLogin = user.LastLogin
            };
        }

        public static User ToDomain(AppUser entity)
        {
            return new User(
               entity.UserName,
                entity.PasswordHash,
                (UserRole)entity.Role,
                entity.CreatedAt
            );
        }
    }
}
