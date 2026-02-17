using Auth.Core.DTOs;
using Auth.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Interfaces
{
    public interface IReadRepository
    {
        Task<User?> GetBUserNameAsync(string email);
        Task<User?> GetByIdAsync(Guid id);
        Task<List<ProductResponse>> GetAllProductsAsync(string? name,
    int? categoryId);
        Task<Product?> GetProductByIdAsync(Guid id);
        Task<List<Category>> GetAllCategoriesAsync();
    }
}
