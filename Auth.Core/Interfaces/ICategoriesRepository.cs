using Auth.Core.Entities;

namespace Auth.Core.Interfaces
{
    public interface ICategoriesRepository
    {

        Task<List<Category>> GetAllCategoriesAsync();
    }
}
