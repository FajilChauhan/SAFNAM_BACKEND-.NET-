using MediatR;
using SafnamBackend.Application.Module.Menu.Command;
using SafnamBackend.Application.Module.Room.Command;
using SafnamBackend.Domain.Interface;

namespace SafnamBackend.Application.Module.Menu.Handler
{
    public class UpdateMenuStatusHandler : IRequestHandler<UpdateMenuStatusCommand, string>
    {
        private readonly IMenuRepository _repo;

        public UpdateMenuStatusHandler(IMenuRepository repo)
        {
            _repo = repo;
        }

        public async Task<string> Handle(UpdateMenuStatusCommand request, CancellationToken cancellationToken)
        {
            await _repo.UpdateStatusAsync(request.Id, request.IsActive);
            return request.IsActive ? "Room Activated" : "Room Deactivated";
        }
    }
}
