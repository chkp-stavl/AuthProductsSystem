using Auth.Core.Entities;

namespace Auth.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByUserNameAsync(string userName);
        Task<User?> GetByIdAsync(Guid id);
        Task UpdateUserLastLogInAsync(User user);
    }
}
