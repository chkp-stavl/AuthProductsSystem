using Auth.Core.Entities;
using Auth.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Core.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(Guid userId, string userName, UserRole role);
    }
}
