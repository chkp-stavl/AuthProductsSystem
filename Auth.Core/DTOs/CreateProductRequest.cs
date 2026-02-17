namespace Auth.Core.DTOs
{
    public class CreateProductRequest
    {
        public string ProductName { get; set; } = "";
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public int UnitsInStock { get; set; }
    }
}
