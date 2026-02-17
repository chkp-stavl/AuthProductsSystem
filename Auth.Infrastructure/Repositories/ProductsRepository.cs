using Auth.Core.DTOs;
using Auth.Core.Entities;
using Auth.Core.Interfaces;
using Auth.Infrastructure.Data;
using Auth.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly AuthDbContext _db;

        public ProductsRepository(AuthDbContext db)
        {
            _db = db;
        }

        public async Task<List<ProductResponse>> GetProudctsAsync(string? name, int? categoryId)
        {
            var query =
                from p in _db.Products.AsNoTracking()
                join c in _db.Categories.AsNoTracking() on p.CategoryId equals c.Id
                select new { p, CategoryName = c.Name };

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(x => x.p.Name.Contains(name));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(x => x.p.CategoryId == categoryId.Value);
            }
            var filterProudcts = await query
                .Select(x => new ProductResponse(
                    x.p.Id,
                    x.p.Name,
                    x.p.CategoryId,
                    x.CategoryName,
                    x.p.Price,
                    x.p.UnitsInStock,
                    x.p.CreatedAt,
                    x.p.UpdatedAt))
                .ToListAsync();

            return filterProudcts;
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            var entity = await _db.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return entity == null ? null : ProductMapper.ToDomain(entity);
        }

        public async Task AddAsync(Product product)
        {
            var entity = ProductMapper.ToEntity(product);
            _db.Products.Add(entity);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            var entity = await _db.Products.FirstOrDefaultAsync(x => x.Id == product.Id);
            if (entity == null)
            {
                return;
            }

            entity.Name = product.ProductName;
            entity.CategoryId = product.CategoryId;
            entity.Price = product.Price;
            entity.UnitsInStock = product.UnitsInStock;
            entity.UpdatedAt = product.UpdatedAt;

            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product product)
        {
            var entity = await _db.Products.FirstOrDefaultAsync(x => x.Id == product.Id);
            if (entity == null)
            {
                return;
            }

            _db.Products.Remove(entity);
            await _db.SaveChangesAsync();
        }
    }
}
