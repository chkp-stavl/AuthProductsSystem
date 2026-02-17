using Auth.Core.Entities;
using Auth.Core.Interfaces;
using Auth.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly IReadRepository _read;

        public CategoriesRepository(IReadRepository read)
        {
            _read = read;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _read.GetAllCategoriesAsync();
        }


    }
}


