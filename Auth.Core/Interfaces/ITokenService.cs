using Auth.Core.Enums;

namespace Auth.Core.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(Guid userId, string userName, UserRole role);
    }
}
