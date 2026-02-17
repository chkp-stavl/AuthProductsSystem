namespace Auth.Infrastructure.Data
{
    public class AppCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";

        public ICollection<AppProduct> Products { get; set; } = new List<AppProduct>();
    }
}
