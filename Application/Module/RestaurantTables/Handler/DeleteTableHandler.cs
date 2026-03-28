using MediatR;
using SafnamBackend.Application.Module.RestaurantTable.Command;
using SafnamBackend.Domain.Interface;

namespace SafnamBackend.Application.Module.RestaurantTables.Handler
{
    public class DeleteTableHandler : IRequestHandler<DeleteTableCommand, string>
    {
        private readonly IRestaurantTableRepository _repo;

        public DeleteTableHandler(IRestaurantTableRepository repo)
        {
            _repo = repo;
        }

        public async Task<string> Handle(DeleteTableCommand request, CancellationToken cancellationToken)
        {
            await _repo.DeleteAsync(request.Id);
            return "Deleted";
        }
    }
}
