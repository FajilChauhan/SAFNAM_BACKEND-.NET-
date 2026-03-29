using MediatR;
using TableModel = SafnamBackend.Domain.Models.RestaurantTable;

namespace SafnamBackend.Application.Module.RestaurantTable.Query
{
    public class GetAllTablesQuery : IRequest<IEnumerable<TableModel>> { }

    public class GetTableByIdQuery : IRequest<TableModel?>
    {
        public int Id { get; set; }
    }
    public class GetActiveTablesQuery : IRequest<IEnumerable<TableModel>> { }
}