using MediatR;
using SafnamBackend.Application.Module.Room.Command;
using SafnamBackend.Domain.Interface;

namespace SafnamBackend.Application.Module.Room.Handler
{
    public class DeleteRoomHandler : IRequestHandler<DeleteRoomCommand, string>
    {
        private readonly IRoomRepository _repo;

        public DeleteRoomHandler(IRoomRepository repo)
        {
            _repo = repo;
        }

        public async Task<string> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            var count = await _repo.GetBookingCountAsync(request.Id);

            if (count > 0)
                return $"Cannot delete. {count} bookings exist for this room.";

            await _repo.DeleteAsync(request.Id);

            return "Deleted";
        }
    }
}
