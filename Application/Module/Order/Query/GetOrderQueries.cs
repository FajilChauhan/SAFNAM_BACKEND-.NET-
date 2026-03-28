using MediatR;
using OrderModel = SafnamBackend.Domain.Models.Order;

namespace SafnamBackend.Application.Module.Order.Query
{
    public class GetAllOrdersQuery : IRequest<IEnumerable<OrderModel>> { }

    public class GetPendingOrdersQuery : IRequest<IEnumerable<OrderModel>> { }

    public class GetOrderByIdQuery : IRequest<object>
    {
        public int Id { get; set; }
    }

    public class GetOrdersByUserIdQuery : IRequest<IEnumerable<OrderModel>>
    {
        public int UserId { get; set; }
    }
}