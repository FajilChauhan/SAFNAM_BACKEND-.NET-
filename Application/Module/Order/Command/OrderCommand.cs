using MediatR;
using OrderModel = SafnamBackend.Domain.Models.Order;
using OrderItemModel = SafnamBackend.Domain.Models.OrderItem;

namespace SafnamBackend.Application.Module.Order.Command
{
    public class CreateOrderCommand : IRequest<object>
    {
        public OrderModel Order { get; set; }
        public List<OrderItemModel> Items { get; set; }
    }

    public class UpdateOrderCommand : IRequest<string>
    {
        public OrderModel Order { get; set; } // partial update allowed
    }
}
