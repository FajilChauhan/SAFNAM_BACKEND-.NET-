using MediatR;

namespace SafnamBackend.Application.Module.RoomBooking.Handler
{
    public class DeleteRoomBookingHandler : IRequestHandler<DeleteRoomBookingCommand, string>
    {
        private readonly IRoomBookingRepository _repo;

        public DeleteRoomBookingHandler(IRoomBookingRepository repo)
        {
            _repo = repo;
        }

        public async Task<string> Handle(DeleteRoomBookingCommand request, CancellationToken cancellationToken)
        {
            await _repo.DeleteAsync(request.Id);
            return "Deleted";
        }
    }
}
