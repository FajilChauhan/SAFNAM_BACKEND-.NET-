using MediatR;
using SafnamBackend.Application.Module.Order.Query;
using SafnamBackend.Domain.Interface;

public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, object>
{
    private readonly IOrderRepository _repo;

    public GetOrderByIdHandler(IOrderRepository repo)
    {
        _repo = repo;
    }

    public async Task<object> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _repo.GetByIdAsync(request.Id);

        if (order == null)
            return null;

        var items = await _repo.GetItemsAsync(request.Id);

        return new
        {
            order,
            items
        };
    }
}