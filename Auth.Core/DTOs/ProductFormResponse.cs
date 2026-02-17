using Auth.Core.Entities;

namespace Auth.Core.DTOs
{
    public class ProductFormResponse
    {
        public List<Category> Categories { get; set; } = new();
    }
}
