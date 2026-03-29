using MediatR;
using SafnamBackend.Application.Module.RestaurantTable.Query;
using SafnamBackend.Domain.Interface;
using TableModel = SafnamBackend.Domain.Models.RestaurantTable;

namespace SafnamBackend.Application.Module.RestaurantTables.Handler
{
    public class GetActiveTablesHandler : IRequestHandler<GetActiveTablesQuery, IEnumerable<TableModel>>
    {
        private readonly IRestaurantTableRepository _repo;

        public GetActiveTablesHandler(IRestaurantTableRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<TableModel>> Handle(GetActiveTablesQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetAllActiveAsync();
        }
    }
}
