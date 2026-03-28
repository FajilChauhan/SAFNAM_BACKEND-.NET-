using MediatR;
using SafnamBackend.Domain.Interface;

namespace SafnamBackend.Application.Module.TableBooking.Handler
{
    public class UpdateTableBookingHandler : IRequestHandler<UpdateTableBookingCommand, string>
    {
        private readonly ITableBookingRepository _repo;

        public UpdateTableBookingHandler(ITableBookingRepository repo)
        {
            _repo = repo;
        }

        public async Task<string> Handle(UpdateTableBookingCommand request, CancellationToken cancellationToken)
        {
            var b = request.Dto;

            var conflict = await _repo.CheckConflictAsync(
                b.TableId,
                b.BookingDate,
                b.TimeSlot,
                b.Id
            );

            if (conflict > 0)
                throw new Exception("Table already booked for this time slot");

            await _repo.UpdateAsync(b);

            return "Updated";
        }
    }
}
