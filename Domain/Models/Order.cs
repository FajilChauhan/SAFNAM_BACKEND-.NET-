namespace SafnamBackend.Domain.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Address { get; set; }
        public decimal TotalAmount { get; set; }
        public string? PaymentStatus { get; set; }
        public string? Status { get; set; }
    }
}
