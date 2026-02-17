using Auth.Core.Entities;
using Auth.Core.Interfaces;
using Auth.Infrastructure.Interfaces;
using Auth.Infrastructure.Repositories.Read;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Repositories
{
    public class UsersRepository : IUserRepository
    {
        private readonly IReadRepository _read;
        private readonly IWriteRepository _write;

        public UsersRepository(IReadRepository read, IWriteRepository write)
        {
            _read = read;
            _write = write;
        }

        public Task<User?> GetByUserNameAsync(string userName)
        {
            return _read.GetBUserNameAsync(userName);
        }

        public Task<User?> GetByIdAsync(Guid id)
        {
            return _read.GetByIdAsync(id);
        }
        public async Task AddAsync(User user)
        {
           await  _write.AddAsync(user);
        }

        public async Task UpdateUserLastLogInAsync(User user)
        {
            var entity = await _read.GetByIdAsync(user.Id);
            if (entity == null) return;

            entity.LastLogin = user.LastLogin;
        }
    }
}
