using MediatR;
using SafnamBackend.Application.Module.Order.Query;
using SafnamBackend.Domain.Interface;
using OrderModel = SafnamBackend.Domain.Models.Order;

public class GetOrdersByUserIdHandler : IRequestHandler<GetOrdersByUserIdQuery, IEnumerable<OrderModel>>
{
    private readonly IOrderRepository _repo;

    public GetOrdersByUserIdHandler(IOrderRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<OrderModel>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
    {
        return await _repo.GetByUserAsync(request.UserId);
    }
}