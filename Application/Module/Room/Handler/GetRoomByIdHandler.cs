using MediatR;
using SafnamBackend.Domain.Interface;
using RoomModel = SafnamBackend.Domain.Models.Room;

namespace SafnamBackend.Application.Module.Room.Handler
{
    public class GetRoomByIdHandler : IRequestHandler<GetRoomByIdQuery, RoomModel?>
    {
        private readonly IRoomRepository _repo;

        public GetRoomByIdHandler(IRoomRepository repo)
        {
            _repo = repo;
        }

        public async Task<RoomModel?> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
        {
            var rooms = await _repo.GetAllAsync();
            return rooms.FirstOrDefault(x => x.Id == request.Id);
        }
    }
}
