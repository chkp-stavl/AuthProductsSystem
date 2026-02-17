using Auth.Core.DTOs;
using Auth.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Core.Interfaces
{
    public interface IProductsRepository
    {
        Task<List<ProductResponse>> GetProudctsAsync(string? name,
    int? categoryId);
        Task<Product?> GetByIdAsync(Guid id);

        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Product product);
    }
}
