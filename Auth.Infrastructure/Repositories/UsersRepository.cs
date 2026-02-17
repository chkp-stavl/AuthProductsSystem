using Auth.Core.Entities;
using Auth.Core.Interfaces;
using Auth.Infrastructure.Data;
using Auth.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Repositories
{
    public class UsersRepository : IUserRepository
    {
        private readonly AuthDbContext _db;

        public UsersRepository(AuthDbContext db)
        {
            _db = db;
        }

        public async Task<User?> GetByUserNameAsync(string userName)
        {
            var entity = await _db.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserName == userName);

            return entity == null ? null : UserMapper.ToDomain(entity);
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            var entity = await _db.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return entity == null ? null : UserMapper.ToDomain(entity);
        }

        public async Task UpdateUserLastLogInAsync(User user)
        {
            var entity = await _db.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
            if (entity == null)
            {
                return;
            }

            entity.LastLogin = user.LastLogin;
            await _db.SaveChangesAsync();
        }
    }
}
