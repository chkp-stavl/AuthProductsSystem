using Auth.Core.Entities;
using Auth.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Data
{
    public class AppProduct
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = "";
        public int CategoryId { get; set; }

        public decimal Price { get; set; }
        public int UnitsInStock { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
