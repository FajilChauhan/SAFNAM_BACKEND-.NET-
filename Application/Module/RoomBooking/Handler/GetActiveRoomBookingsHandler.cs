using MediatR;
using RoomBookingModel = SafnamBackend.Domain.Models.RoomBooking;

namespace SafnamBackend.Application.Module.RoomBooking.Handler
{
    public class GetActiveRoomBookingsHandler : IRequestHandler<GetActiveRoomBookingsQuery, IEnumerable<RoomBookingModel>>
    {
        private readonly IRoomBookingRepository _repo;

        public GetActiveRoomBookingsHandler(IRoomBookingRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<RoomBookingModel>> Handle(GetActiveRoomBookingsQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetActiveAsync();
        }
    }
}
