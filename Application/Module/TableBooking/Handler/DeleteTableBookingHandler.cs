using MediatR;
using SafnamBackend.Domain.Interface;

namespace SafnamBackend.Application.Module.RoomBooking.Handler
{
    public class DeleteTableBookingHandler : IRequestHandler<DeleteTableBookingCommand, string>
    {
        private readonly ITableBookingRepository _repo;

        public DeleteTableBookingHandler(ITableBookingRepository repo)
        {
            _repo = repo;
        }

        public async Task<string> Handle(DeleteTableBookingCommand request, CancellationToken cancellationToken)
        {
            await _repo.DeleteAsync(request.Id);
            return "Deleted";
        }
    }
}
