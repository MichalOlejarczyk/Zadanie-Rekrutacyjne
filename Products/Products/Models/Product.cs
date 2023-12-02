namespace Products.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name  { get; set; }

        public int Cost { get; set; }
        public int InStock { get; set; }
        public string? Description { get; set; }

       
    }
}
