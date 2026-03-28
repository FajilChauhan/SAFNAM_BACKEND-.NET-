using MediatR;
using RoomBookingModel = SafnamBackend.Domain.Models.RoomBooking;

namespace SafnamBackend.Application.Module.RoomBooking.Handler
{
    public class GetRoomBookingsByUserHandler : IRequestHandler<GetRoomBookingsByUserQuery, IEnumerable<RoomBookingModel>>
    {
        private readonly IRoomBookingRepository _repo;

        public GetRoomBookingsByUserHandler(IRoomBookingRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<RoomBookingModel>> Handle(GetRoomBookingsByUserQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetByUserAsync(request.UserId);
        }
    }
}
