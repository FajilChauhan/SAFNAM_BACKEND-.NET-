using MediatR;

namespace SafnamBackend.Application.Module.RoomBooking.Handler
{
    public class UpdateRoomBookingHandler : IRequestHandler<UpdateRoomBookingCommand, string>
    {
        private readonly IRoomBookingRepository _repo;

        public UpdateRoomBookingHandler(IRoomBookingRepository repo)
        {
            _repo = repo;
        }

        public async Task<string> Handle(UpdateRoomBookingCommand request, CancellationToken cancellationToken)
        {
            var b = request.Dto;

            if (b.CheckOut <= b.CheckIn)
                throw new Exception("Check-out must be after check-in");

            // 🔥 CONFLICT CHECK (exclude current booking)
            var conflict = await _repo.CheckConflictAsync(
                b.RoomId,
                b.CheckIn,
                b.CheckOut,
                b.Id // exclude itself
            );

            if (conflict > 0)
                throw new Exception("Room already booked for selected dates");

            await _repo.UpdateAsync(b);

            return "Updated";
        }
    }
}
