using MediatR;
using TableModel = SafnamBackend.Domain.Models.RestaurantTable;

namespace SafnamBackend.Application.Module.RestaurantTable.Command
{
    public class CreateTableCommand : IRequest<string>
    {
        public TableModel Table { get; set; }
    }

    public class UpdateTableStatusCommand : IRequest<string>
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
    }

    public class UpdateTableCommand : IRequest<string>
    {
        public TableModel Table { get; set; }
    }

    public class DeleteTableCommand : IRequest<string>
    {
        public int Id { get; set; }
    }
}