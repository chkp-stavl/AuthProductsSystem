using Auth.Core.DTOs;
using Auth.Core.Entities;
using Auth.Core.Interfaces;
using Auth.Infrastructure.Data;
using Auth.Infrastructure.Interfaces;
using Auth.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly IReadRepository _read;
        private readonly IWriteRepository _write;

        public ProductsRepository(IReadRepository read, IWriteRepository write)
        {
            _read = read;
            _write = write;
        }

        public async Task<List<ProductResponse>> GetProudctsAsync(string? name,
    int? categoryId)
        {
            return await _read.GetAllProductsAsync(name, categoryId);
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _read.GetProductByIdAsync(id);
        }

        public async Task AddAsync(Product product)
        {
            await _write.AddProductAsync(product);
        }

        public async Task UpdateAsync(Product product)
        {
            await _write.UpdateProductAsync(product);
        }

        public async Task DeleteAsync(Product product)
        {
            await _write.DeleteProductAsync(product);
        }
    }
}
