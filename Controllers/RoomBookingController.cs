using MediatR;
using Microsoft.AspNetCore.Mvc;
using SafnamBackend.DTO;

[Route("api/roombooking")]
[ApiController]
public class RoomBookingController : ControllerBase
{
    private readonly IMediator _mediator;

    public RoomBookingController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _mediator.Send(new GetAllRoomBookingsQuery()));
    }

    [HttpGet("room/{roomId}")]
    public async Task<IActionResult> GetByRoom(int roomId)
    {
        return Ok(await _mediator.Send(new GetRoomBookingsByRoomQuery
        {
            RoomId = roomId
        }));
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetActive()
    {
        return Ok(await _mediator.Send(new GetActiveRoomBookingsQuery()));
    }

    // 🔥 USER HISTORY
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUser(int userId)
    {
        return Ok(await _mediator.Send(new GetRoomBookingsByUserQuery
        {
            UserId = userId
        }));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RoomBookingDto dto)
    {
        return Ok(await _mediator.Send(new CreateRoomBookingCommand
        {
            Dto = dto
        }));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] RoomBookingDto dto)
    {
        dto.Id = id;

        return Ok(await _mediator.Send(new UpdateRoomBookingCommand
        {
            Dto = dto
        }));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(await _mediator.Send(new DeleteRoomBookingCommand
        {
            Id = id
        }));
    }
}