using Products.Repository;

namespace Products.Models.Entities
{
    public class ProductEntity : Entity
    {
        public override Guid Id { get; set; }

        public string Name { get; set; }

        public int Cost { get; set; }
        public int InStock { get; set; }
        public string? Description { get; set; }
    }
}
