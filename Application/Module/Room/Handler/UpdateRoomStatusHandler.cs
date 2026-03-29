using MediatR;
using SafnamBackend.Application.Module.Room.Command;
using SafnamBackend.Domain.Interface;

namespace SafnamBackend.Application.Module.Room.Handler
{
    public class UpdateRoomStatusHandler : IRequestHandler<UpdateRoomStatusCommand, string>
    {
        private readonly IRoomRepository _repo;

        public UpdateRoomStatusHandler(IRoomRepository repo)
        {
            _repo = repo;
        }

        public async Task<string> Handle(UpdateRoomStatusCommand request, CancellationToken cancellationToken)
        {
            await _repo.UpdateStatusAsync(request.Id, request.IsActive);
            return request.IsActive ? "Room Activated" : "Room Deactivated";
        }
    }
}
