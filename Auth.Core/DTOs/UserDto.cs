using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Core.DTOs
{

    public record UserDto(
        Guid Id,
        string Email,
        string FirstName,
        string LastName,
        DateTime CreatedAt);
}
