using Auth.Core.DTOs;
using Auth.Core.Entities;
using Auth.Infrastructure.Data;
using Auth.Infrastructure.Interfaces;
using Auth.Infrastructure.Mappers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Repositories.Read
{
    public class ReadRepository : IReadRepository
    {
        private readonly AuthDbContext _db;

        public ReadRepository(AuthDbContext db)
        {
            _db = db;
        }

        public async Task<User?> GetBUserNameAsync(string userName)
        {
            var entity = await _db.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserName == userName);

            return entity == null ? null : UserMapper.ToDomain(entity);
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            var entity = await _db.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return entity == null ? null : UserMapper.ToDomain(entity);
        }

        public async Task<List<ProductResponse>> GetAllProductsAsync(string? name,
    int? categoryId)
        {
            var query =
        from p in _db.Products
        join c in _db.Categories
            on p.CategoryId equals c.Id
        select new
        {
            Product = p,
            CategoryName = c.Name
        };

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(x =>
                    x.Product.Name.Contains(name));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(x =>
                    x.Product.CategoryId == categoryId.Value);
            }

            var products = await query
                .Select(x => new ProductResponse(
                    x.Product.Id,
                    x.Product.Name,
                    x.Product.CategoryId,
                    x.CategoryName,
                    x.Product.Price,
                    x.Product.UnitsInStock,
                    x.Product.CreatedAt,
                    x.Product.UpdatedAt
                ))
                .AsNoTracking()
                .ToListAsync();

            return products;
        }

        public async Task<Product?> GetProductByIdAsync(Guid id)
        {
            var entity = await _db.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return entity == null ? null : ProductMapper.ToDomain(entity);
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            var categories = await _db.Categories
                .AsNoTracking(). Select(c => new Category(c.Id, c.Name))
                .ToListAsync();
            return categories;
        }
    }
}
