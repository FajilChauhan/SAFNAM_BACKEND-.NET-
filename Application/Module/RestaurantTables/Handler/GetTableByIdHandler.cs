using MediatR;
using SafnamBackend.Application.Module.RestaurantTable.Query;
using SafnamBackend.Domain.Interface;
using TableModel = SafnamBackend.Domain.Models.RestaurantTable;

namespace SafnamBackend.Application.Module.RestaurantTables.Handler
{
    public class GetTableByIdHandler : IRequestHandler<GetTableByIdQuery, TableModel?>
    {
        private readonly IRestaurantTableRepository _repo;

        public GetTableByIdHandler(IRestaurantTableRepository repo)
        {
            _repo = repo;
        }

        public async Task<TableModel?> Handle(GetTableByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetByIdAsync(request.Id);
        }
    }
}
