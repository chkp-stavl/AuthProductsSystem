using Auth.Core.Entities;
using Auth.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Security
{
    public class PasswordHasher : IPasswordHasher
    {
        private readonly PasswordHasher<User> _hasher = new();

        public string Hash(string password)
        {
            return _hasher.HashPassword(null!, password);
        }

        public bool Verify(string hash, string password)
        {
            var result = _hasher.VerifyHashedPassword(null!, hash, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}
