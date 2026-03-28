using MediatR;
using Microsoft.AspNetCore.Mvc;
using SafnamBackend.DTO;

[Route("api/tablebooking")]
[ApiController]
public class TableBookingController : ControllerBase
{
    private readonly IMediator _mediator;

    public TableBookingController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _mediator.Send(new GetAllTableBookingsQuery()));

    // 🔥 ACTIVE ONLY (GRID)
    [HttpGet("active")]
    public async Task<IActionResult> GetActive()
        => Ok(await _mediator.Send(new GetActiveTableBookingsQuery()));

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUser(int userId)
        => Ok(await _mediator.Send(new GetTableBookingsByUserQuery { UserId = userId }));

    [HttpGet("table/{tableId}")]
    public async Task<IActionResult> GetByTable(int tableId)
        => Ok(await _mediator.Send(new GetTableBookingsByTableQuery { TableId = tableId }));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TableBookingDTO dto)
        => Ok(await _mediator.Send(new CreateTableBookingCommand { Dto = dto }));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] TableBookingDTO dto)
    {
        dto.Id = id;
        return Ok(await _mediator.Send(new UpdateTableBookingCommand { Dto = dto }));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
        => Ok(await _mediator.Send(new DeleteTableBookingCommand { Id = id }));
}