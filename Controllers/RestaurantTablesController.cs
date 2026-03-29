using MediatR;
using Microsoft.AspNetCore.Mvc;
using SafnamBackend.Application.Module.RestaurantTable.Command;
using SafnamBackend.Application.Module.RestaurantTable.Query;
using SafnamBackend.Application.Module.Room.Command;
using SafnamBackend.Domain.Models;

[Route("api/[controller]")]
[ApiController]
public class RestaurantTablesController : ControllerBase
{
    private readonly IMediator _mediator;

    public RestaurantTablesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET ALL
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _mediator.Send(new GetAllTablesQuery()));
    }

    [HttpGet("Active")]
    public async Task<IActionResult> GetActive()
    {
        return Ok(await _mediator.Send(new GetActiveTablesQuery()));
    }

    // GET BY ID
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _mediator.Send(new GetTableByIdQuery { Id = id });

        if (result == null) return NotFound();

        return Ok(result);
    }

    // CREATE
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RestaurantTable table)
    {
        try
        {
            return Ok(await _mediator.Send(new CreateTableCommand
            {
                Table = table
            }));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message); // ✅ clean error
        }
    }

    // UPDATE
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] RestaurantTable table)
    {
        try
        {
            table.Id = id;

            return Ok(await _mediator.Send(new UpdateTableCommand
            {
                Table = table
            }));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message); // ✅ clean error
        }
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(int id, bool isActive)
    {
        try
        {
            var result = await _mediator.Send(new UpdateTableStatusCommand
            {
                Id = id,
                IsActive = isActive
            });

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message); // ✅ clean error
        }
    }

    // DELETE
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(await _mediator.Send(new DeleteTableCommand
        {
            Id = id
        }));
    }
}