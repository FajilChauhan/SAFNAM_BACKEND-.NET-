using MediatR;
using RoomBookingModel = SafnamBackend.Domain.Models.RoomBooking;

namespace SafnamBackend.Application.Module.RoomBooking.Handler
{
    public class GetAllRoomBookingsHandler : IRequestHandler<GetAllRoomBookingsQuery, IEnumerable<RoomBookingModel>>
    {
        private readonly IRoomBookingRepository _repo;

        public GetAllRoomBookingsHandler(IRoomBookingRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<RoomBookingModel>> Handle(GetAllRoomBookingsQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetAllAsync();
        }
    }
}
