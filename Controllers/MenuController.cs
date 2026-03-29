using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafnamBackend.Application.Module.Menu.Command;
using SafnamBackend.Application.Module.Menu.Query;
using SafnamBackend.Application.Module.Room.Command;
using SafnamBackend.Domain.Models;

[Route("api/[controller]")]
[ApiController]
public class MenuController : ControllerBase
{
    private readonly IMediator _mediator;

    public MenuController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // ✅ GET ALL
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _mediator.Send(new GetMenuQuery()));
    }

    [HttpGet("Active")]
    public async Task<IActionResult> GetActive()
    {
        return Ok(await _mediator.Send(new GetActiveMenuQuery()));
    }

    // ✅ GET BY ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetMenuByIdQuery { Id = id });

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] Menu menu, IFormFile image)
    {
        try
        {
            var result = await _mediator.Send(new CreateMenuCommand
            {
                Menu = menu,
                Image = image
            });

            return Ok(result);
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
            var result = await _mediator.Send(new UpdateMenuStatusCommand
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

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromForm] Menu menu, IFormFile? image)
    {
        menu.Id = id;
        try
        {
            var result = await _mediator.Send(new UpdateMenuCommand
            {
                Menu = menu,
                Image = image
            });

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message); // ✅ clean error
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var result = await _mediator.Send(new DeleteMenuCommand { Id = id });
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message); // ✅ clean error
        }
    }
}