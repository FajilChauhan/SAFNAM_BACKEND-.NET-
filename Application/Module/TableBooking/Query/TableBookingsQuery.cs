using MediatR;
using SafnamBackend.Domain.Models;

public class GetAllTableBookingsQuery : IRequest<IEnumerable<TableBooking>> { }

public class GetActiveTableBookingsQuery : IRequest<IEnumerable<TableBooking>> { }

public class GetTableBookingsByUserQuery : IRequest<IEnumerable<TableBooking>>
{
    public int UserId { get; set; }
}

public class GetTableBookingsByTableQuery : IRequest<IEnumerable<TableBooking>>
{
    public int TableId { get; set; }
}