using Auth.Core.Entities;
using Auth.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Mappers
{
    public static class ProductMapper
    {
        public static AppProduct ToEntity(Product p)
        {
            return new AppProduct
            {
                Id = p.Id,
                Name = p.ProductName,
                CategoryId = p.CategoryId,
                Price = p.Price,
                UnitsInStock = p.UnitsInStock,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            };
        }

        public static Product ToDomain(AppProduct p)
        {
            return new Product(
                p.Id,
                p.Name,
                p.CategoryId,
                p.Price,
                p.UnitsInStock,
                p.CreatedAt,
                p.UpdatedAt
            );
        }
    }
}
