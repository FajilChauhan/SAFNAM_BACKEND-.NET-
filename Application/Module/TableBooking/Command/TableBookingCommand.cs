using MediatR;
using SafnamBackend.DTO;

public class CreateTableBookingCommand : IRequest<string>
{
    public TableBookingDTO Dto { get; set; }
}

public class UpdateTableBookingCommand : IRequest<string>
{
    public TableBookingDTO Dto { get; set; }
}

public class DeleteTableBookingCommand : IRequest<string>
{
    public int Id { get; set; }
}