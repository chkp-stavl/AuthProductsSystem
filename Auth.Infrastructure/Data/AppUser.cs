using Auth.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Data
{
    public class AppUser
    {
        public Guid Id { get; set; }

        public string UserName { get; set; } = "";
        public string PasswordHash { get; set; } = "";

        public int Role { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? LastLogin { get; set; }
    }


}
