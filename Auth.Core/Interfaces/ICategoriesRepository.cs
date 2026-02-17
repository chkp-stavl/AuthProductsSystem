using Auth.Core.DTOs;
using Auth.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Core.Interfaces
{
    public interface ICategoriesRepository
    {

        Task<List<Category>> GetAllCategoriesAsync();
    }
}
