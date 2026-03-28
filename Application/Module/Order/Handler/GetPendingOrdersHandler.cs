using MediatR;
using SafnamBackend.Application.Module.Order.Query;
using SafnamBackend.Domain.Interface;
using OrderModel = SafnamBackend.Domain.Models.Order;

public class GetPendingOrdersHandler : IRequestHandler<GetPendingOrdersQuery, IEnumerable<OrderModel>>
{
    private readonly IOrderRepository _repo;

    public GetPendingOrdersHandler(IOrderRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<OrderModel>> Handle(GetPendingOrdersQuery request, CancellationToken cancellationToken)
    {
        return await _repo.GetPendingAsync();
    }
}