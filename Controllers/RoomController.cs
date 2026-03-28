using MediatR;
using Microsoft.AspNetCore.Mvc;
using SafnamBackend.Application.DTO;
using SafnamBackend.Application.Module.Room.Command;

[Route("api/room")]
[ApiController]
public class RoomController : ControllerBase
{
    private readonly IMediator _mediator;

    public RoomController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _mediator.Send(new GetRoomsQuery()));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetRoomByIdQuery { Id = id });

        if (result == null) return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] RoomDto dto)
    {
        return Ok(await _mediator.Send(new CreateRoomCommand { Dto = dto }));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromForm] RoomDto dto)
    {
        dto.Id = id;
        return Ok(await _mediator.Send(new UpdateRoomCommand { Dto = dto }));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(await _mediator.Send(new DeleteRoomCommand { Id = id }));
    }

    // 🔥 NEW (reference check API)
    [HttpGet("{id}/booking-count")]
    public async Task<IActionResult> GetBookingCount(int id)
    {
        return Ok(await _mediator.Send(new GetRoomBookingCountQuery
        {
            RoomId = id
        }));
    }
}