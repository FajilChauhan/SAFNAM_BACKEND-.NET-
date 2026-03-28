using MediatR;
using RoomBookingModel = SafnamBackend.Domain.Models.RoomBooking;

namespace SafnamBackend.Application.Module.RoomBooking.Handler
{
    public class GetRoomBookingsByRoomHandler : IRequestHandler<GetRoomBookingsByRoomQuery, IEnumerable<RoomBookingModel>>
    {
        private readonly IRoomBookingRepository _repo;

        public GetRoomBookingsByRoomHandler(IRoomBookingRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<RoomBookingModel>> Handle(GetRoomBookingsByRoomQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetByRoomAsync(request.RoomId);
        }
    }
}
