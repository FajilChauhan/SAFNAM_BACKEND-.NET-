using MediatR;
using SafnamBackend.Domain.Interface;

namespace SafnamBackend.Application.Module.TableBooking.Handler
{
    public class CreateTableBookingHandler : IRequestHandler<CreateTableBookingCommand, string>
    {
        private readonly ITableBookingRepository _repo;

        public CreateTableBookingHandler(ITableBookingRepository repo)
        {
            _repo = repo;
        }

        public async Task<string> Handle(CreateTableBookingCommand request, CancellationToken cancellationToken)
        {
            var b = request.Dto;

            // 🔥 TIME VALIDATION
            var time = TimeSpan.Parse(b.TimeSlot);

            bool valid =
                (time >= TimeSpan.FromHours(11) && time < TimeSpan.FromHours(15)) ||
                (time >= TimeSpan.FromHours(19) && time < TimeSpan.FromHours(23));

            if (!valid)
                throw new Exception("Invalid booking time (Allowed: 11-3 & 7-11)");

            // 🔥 CONFLICT CHECK
            var conflict = await _repo.CheckConflictAsync(b.TableId, b.BookingDate, b.TimeSlot);

            if (conflict > 0)
                throw new Exception("Table already booked for this time slot");

            await _repo.CreateAsync(b);

            return "Booked";
        }
    }
}
