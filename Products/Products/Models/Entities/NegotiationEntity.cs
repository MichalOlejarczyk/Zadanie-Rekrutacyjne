using Products.Repository;

namespace Products.Models.Entities
{
    public class NegotiationEntity : Entity
    {
        public override Guid Id { get; set; }

        public Guid ProductId { get; set; }
        public Guid CustomerId { get; set; }
        public string? Status { get; set; }
        public int NewCost { get; set; }
    }
}
