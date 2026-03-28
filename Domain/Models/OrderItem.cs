namespace SafnamBackend.Domain.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int MenuId { get; set; }
        public int Quantity { get; set; }
        public string? ItemName { get; set; }
    }
}
