using MediatR;
using SafnamBackend.Domain.Interface;
using RoomModel = SafnamBackend.Domain.Models.Room;

namespace SafnamBackend.Application.Module.Room.Handler
{
    public class GetRoomsHandler : IRequestHandler<GetRoomsQuery, IEnumerable<RoomModel>>
    {
        private readonly IRoomRepository _repo;

        public GetRoomsHandler(IRoomRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<RoomModel>> Handle(GetRoomsQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetAllAsync();
        }
    }
}
