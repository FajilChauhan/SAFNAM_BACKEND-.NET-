using MediatR;
using SafnamBackend.Application.Module.RestaurantTable.Query;
using SafnamBackend.Domain.Interface;
using TableModel = SafnamBackend.Domain.Models.RestaurantTable;

namespace SafnamBackend.Application.Module.RestaurantTables.Handler
{
    public class GetAllTablesHandler : IRequestHandler<GetAllTablesQuery, IEnumerable<TableModel>>
    {
        private readonly IRestaurantTableRepository _repo;

        public GetAllTablesHandler(IRestaurantTableRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<TableModel>> Handle(GetAllTablesQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetAllAsync();
        }
    }
}
