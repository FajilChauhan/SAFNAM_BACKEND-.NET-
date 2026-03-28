using MediatR;

namespace SafnamBackend.Application.Module.RoomBooking.Handler
{
    public class CreateRoomBookingHandler : IRequestHandler<CreateRoomBookingCommand, object>
    {
        private readonly IRoomBookingRepository _repo;

        public CreateRoomBookingHandler(IRoomBookingRepository repo)
        {
            _repo = repo;
        }

        public async Task<object> Handle(CreateRoomBookingCommand request, CancellationToken cancellationToken)
        {
            var b = request.Dto;

            if (b.CheckOut <= b.CheckIn)
                throw new Exception("Check-out must be after check-in");

            // 🔥 CONFLICT CHECK
            var conflict = await _repo.CheckConflictAsync(b.RoomId, b.CheckIn, b.CheckOut);

            if (conflict > 0)
                throw new Exception("Room already booked for selected dates");

            var nights = (b.CheckOut - b.CheckIn).Days;

            var price = await _repo.GetRoomPriceAsync(b.RoomId);

            if (price == 0)
                throw new Exception("Invalid room");

            var total = nights * price;

            await _repo.CreateAsync(b, total);

            return new
            {
                message = "Room booked successfully",
                totalAmount = total
            };
        }
    }
}
