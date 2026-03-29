using MediatR;
using SafnamBackend.Domain.Models;

public class GetRoomsQuery : IRequest<IEnumerable<Room>> { }

public class GetRoomByIdQuery : IRequest<Room?>
{
    public int Id { get; set; }
}
public class GetRoomBookingCountQuery : IRequest<int>
{
    public int RoomId { get; set; }
}

public class GetActiveRoomsQuery : IRequest<IEnumerable<Room>> { }