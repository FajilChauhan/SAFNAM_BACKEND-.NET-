using MediatR;
using SafnamBackend.Domain.Interface;

namespace SafnamBackend.Application.Module.Room.Handler
{
    public class GetRoomBookingCountHandler : IRequestHandler<GetRoomBookingCountQuery, int>
    {
        private readonly IRoomRepository _repo;

        public GetRoomBookingCountHandler(IRoomRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> Handle(GetRoomBookingCountQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetBookingCountAsync(request.RoomId);
        }
    }
}
