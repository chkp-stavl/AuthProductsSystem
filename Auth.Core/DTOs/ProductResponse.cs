using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Core.DTOs
{
    public class ProductResponse
    {
        public Guid Id { get; private set; }

        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public decimal Price { get; set; }
        public int UnitsInStock { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public ProductResponse(Guid id, string productName, int categoryId, string categoryName, decimal price, int unitsInStock, DateTime createdAt, DateTime? updatedAt)
        {
            Id = id;
            ProductName = productName;
            CategoryId = categoryId;
            CategoryName = categoryName;
            Price = price;
            UnitsInStock = unitsInStock;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
