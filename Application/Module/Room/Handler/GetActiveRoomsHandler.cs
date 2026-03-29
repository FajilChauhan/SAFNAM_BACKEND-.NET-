using MediatR;
using SafnamBackend.Domain.Interface;
using RoomModel = SafnamBackend.Domain.Models.Room;
namespace SafnamBackend.Application.Module.Room.Handler
{
    public class GetActiveRoomsHandler : IRequestHandler<GetActiveRoomsQuery, IEnumerable<RoomModel>>
    {
        private readonly IRoomRepository _repo;

        public GetActiveRoomsHandler(IRoomRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<RoomModel>> Handle(GetActiveRoomsQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetAllActiveAsync();
        }
    }
}
