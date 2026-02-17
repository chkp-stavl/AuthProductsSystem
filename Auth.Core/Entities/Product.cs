using Auth.Core.Enums;

namespace Auth.Core.Entities;

public class Product
{
    public Guid Id { get; private set; }

    public string ProductName { get; set; }
    public int CategoryId { get; set; }

    public decimal Price { get; set; }
    public int UnitsInStock { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }


    public Product(Guid id, string name, int categoryId, decimal price, int stock, DateTime createdAt, DateTime? updatedAt)
    {
        Id = id;
        ProductName = name;
        CategoryId = categoryId;
        Price = price;
        UnitsInStock = stock;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;

    }

    public Product(string productName, int categoryId, decimal price, int unitsInStock)
    {
        Id = Guid.NewGuid(); 
        ProductName = productName;
        CategoryId = categoryId;
        Price = price;
        UnitsInStock = unitsInStock;
        CreatedAt = DateTime.UtcNow;
    }
}
