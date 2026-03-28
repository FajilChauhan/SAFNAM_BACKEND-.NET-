using MediatR;
using SafnamBackend.Domain.Models;

public class GetAllRoomBookingsQuery : IRequest<IEnumerable<RoomBooking>> { }

public class GetRoomBookingsByRoomQuery : IRequest<IEnumerable<RoomBooking>>
{
    public int RoomId { get; set; }
}

public class GetActiveRoomBookingsQuery : IRequest<IEnumerable<RoomBooking>> { }
public class GetRoomBookingsByUserQuery : IRequest<IEnumerable<RoomBooking>> // 🔥 history
{
    public int UserId { get; set; }
}