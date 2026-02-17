namespace Auth.Infrastructure.Data
{
    public class AppProduct
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = "";
        public int CategoryId { get; set; }

        public decimal Price { get; set; }
        public int UnitsInStock { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
