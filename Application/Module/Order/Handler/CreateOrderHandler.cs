using Dapper;
using MediatR;
using SafnamBackend.Application.Module.Order.Command;
using SafnamBackend.Data;
using SafnamBackend.Domain.Interface;

namespace SafnamBackend.Application.Module.Order.Handler
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, object>
    {
        private readonly IOrderRepository _repo;
        private readonly DapperContext _c;

        public CreateOrderHandler(IOrderRepository repo, DapperContext c)
        {
            _repo = repo;
            _c = c;
        }

        public async Task<object> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            using var con = _c.CreateConnection();

            decimal total = 0;

            foreach (var item in request.Items)
            {
                var price = await con.ExecuteScalarAsync<decimal>(
                    "SELECT Price FROM Menu WHERE Id=@id",
                    new { id = item.MenuId });

                total += price * item.Quantity;
            }

            request.Order.TotalAmount = total;
            request.Order.Status = "Pending";
            request.Order.PaymentStatus = "Pending";

            var orderId = await _repo.CreateOrderAsync(request.Order);

            foreach (var item in request.Items)
            {
                item.OrderId = orderId;
                await _repo.AddItemAsync(item);
            }

            return new { orderId, total };
        }
    }
}
