using MediatR;
using SafnamBackend.Application.Module.RestaurantTable.Command;
using SafnamBackend.Application.Module.Room.Command;
using SafnamBackend.Domain.Interface;

namespace SafnamBackend.Application.Module.RestaurantTables.Handler
{
    public class UpdateTableStatusHandler : IRequestHandler<UpdateTableStatusCommand, string>
    {
        private readonly IRestaurantTableRepository _repo;

        public UpdateTableStatusHandler(IRestaurantTableRepository repo)
        {
            _repo = repo;
        }

        public async Task<string> Handle(UpdateTableStatusCommand request, CancellationToken cancellationToken)
        {
            await _repo.UpdateStatusAsync(request.Id, request.IsActive);
            return request.IsActive ? "Room Activated" : "Room Deactivated";
        }
    }
}
