using MediatR;
using SafnamBackend.Application.Module.RestaurantTable.Command;
using SafnamBackend.Domain.Interface;

namespace SafnamBackend.Application.Module.RestaurantTables.Handler
{
    public class UpdateTableHandler : IRequestHandler<UpdateTableCommand, string>
    {
        private readonly IRestaurantTableRepository _repo;

        public UpdateTableHandler(IRestaurantTableRepository repo)
        {
            _repo = repo;
        }

        public async Task<string> Handle(UpdateTableCommand request, CancellationToken cancellationToken)
        {
            await _repo.UpdateAsync(request.Table);
            return "Updated";
        }
    }
}
