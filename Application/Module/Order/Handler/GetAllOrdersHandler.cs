using MediatR;
using SafnamBackend.Application.Module.Order.Query;
using SafnamBackend.Domain.Interface;
using OrderModel = SafnamBackend.Domain.Models.Order;

public class GetAllOrdersHandler : IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderModel>>
{
    private readonly IOrderRepository _repo;

    public GetAllOrdersHandler(IOrderRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<OrderModel>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        return await _repo.GetAllAsync();
    }
}