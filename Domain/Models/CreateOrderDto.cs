namespace SafnamBackend.Domain.Models
{
    public class CreateOrderDto
    {
        public Order Order { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}
