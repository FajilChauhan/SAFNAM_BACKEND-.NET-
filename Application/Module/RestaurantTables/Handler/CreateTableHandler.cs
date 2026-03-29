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
            // 🔥 DUPLICATE CHECK (IMPORTANT)
            var allTables = await _repo.GetAllAsync();

            if (allTables.Any(m => (m.Floor == request.Table.Floor && m.TableNo == request.Table.TableNo)))
            {
                throw new Exception("Table No already exists");
            }
            await _repo.CreateAsync(request.Table);
            return "Table Created";
        }
    }
}
