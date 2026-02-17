using Auth.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Core.DTOs
{
    public class ProductFormResponse
    {
        public List<Category> Categories { get; set; } = new();
    }
}
