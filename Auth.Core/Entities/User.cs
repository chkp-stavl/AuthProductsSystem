using Auth.Core.Enums;

namespace Auth.Core.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string UserName { get; private set; }
        public string PasswordHash { get; private set; }
        public UserRole Role { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? LastLogin { get; set; }

        public User(string userName, string passwordHash, UserRole role)
        {
            Id = Guid.NewGuid();
            UserName = userName;
            PasswordHash = passwordHash;
            Role = role;
            CreatedAt = DateTime.UtcNow;
        }

        public User(Guid id, string userName, string passwordHash, UserRole role, DateTime createdAt, DateTime? lastLogin)
        {
            Id = id;
            UserName = userName;
            PasswordHash = passwordHash;
            Role = role;
            CreatedAt = createdAt;
            LastLogin = lastLogin;
        }
    }
}
