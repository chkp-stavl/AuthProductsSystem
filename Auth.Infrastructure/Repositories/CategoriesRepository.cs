using Auth.Core.Entities;
using Auth.Core.Interfaces;
using Auth.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly AuthDbContext _db;

        public CategoriesRepository(AuthDbContext db)
        {
            _db = db;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            var categories = await _db.Categories
                .AsNoTracking()
                .Select(c => new Category(c.Id, c.Name))
                .ToListAsync();
            return categories;
        }
    }
}
