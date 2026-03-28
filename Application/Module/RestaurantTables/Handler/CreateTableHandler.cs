using MediatR;
using SafnamBackend.Application.Module.RestaurantTable.Command;
using SafnamBackend.Domain.Interface;

namespace SafnamBackend.Application.Module.RestaurantTables.Handler
{
    public class CreateTableHandler : IRequestHandler<CreateTableCommand, string>
    {
        private readonly IRestaurantTableRepository _repo;

        public CreateTableHandler(IRestaurantTableRepository repo)
        {
            _repo = repo;
        }

        public async Task<string> Handle(CreateTableCommand request, CancellationToken cancellationToken)
        {
            await _repo.CreateAsync(request.Table);
            return "Table Created";
        }
    }
}
