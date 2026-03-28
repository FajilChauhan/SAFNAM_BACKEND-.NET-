using MediatR;
using SafnamBackend.DTO;

public class CreateRoomBookingCommand : IRequest<object>
{
    public RoomBookingDto Dto { get; set; }
}

public class UpdateRoomBookingCommand : IRequest<string>
{
    public RoomBookingDto Dto { get; set; }
}

public class DeleteRoomBookingCommand : IRequest<string>
{
    public int Id { get; set; }
}