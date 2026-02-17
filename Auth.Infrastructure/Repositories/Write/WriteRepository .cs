using Auth.Core.Entities;
using Auth.Infrastructure.Data;
using Auth.Infrastructure.Interfaces;
using Auth.Infrastructure.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Repositories.Write
{
    public class UserWriteRepository : IWriteRepository
    {
        private readonly AuthDbContext _db;

        public UserWriteRepository(AuthDbContext db)
        {
            _db = db;
        }



        public async Task AddAsync(User user)
        {
            var entity = UserMapper.ToEntity(user);
            _db.Users.Add(entity);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            var entity = UserMapper.ToEntity(user);
            _db.Users.Update(entity);
            await _db.SaveChangesAsync();
        }

        public async Task AddProductAsync(Product product)
        {
            var entity = ProductMapper.ToEntity(product);
            await _db.Products.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            var entity = ProductMapper.ToEntity(product);
            _db.Products.Update(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(Product product)
        {
            var entity = await _db.Products.FindAsync(product.Id);

            if (entity != null)
            {
                _db.Products.Remove(entity);
                await _db.SaveChangesAsync();
            }
        }

    }
}
