namespace Products.Models
{
    public class Negotiation
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }
        public Guid CustomerId { get; set; }
        public string? Status { get; set; }
        public int NewCost { get; set; }
    }
}
