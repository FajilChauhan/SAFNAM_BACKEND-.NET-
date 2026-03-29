using MediatR;
using SafnamBackend.Application.DTO;

namespace SafnamBackend.Application.Module.Room.Command
{
    public class CreateRoomCommand : IRequest<string>
    {
        public RoomDto Dto { get; set; }
    }

    public class UpdateRoomStatusCommand : IRequest<string>
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
    }
    public class UpdateRoomCommand : IRequest<string>
    {
        public RoomDto Dto { get; set; }
    }

    public class DeleteRoomCommand : IRequest<string>
    {
        public int Id { get; set; }
    }
}
