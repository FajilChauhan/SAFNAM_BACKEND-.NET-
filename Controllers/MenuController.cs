using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafnamBackend.Application.Module.Menu.Command;
using SafnamBackend.Application.Module.Menu.Query;
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

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _mediator.Send(new GetMenuQuery());
        return Ok(result);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromForm] Menu menu, IFormFile image)
    {
        var result = await _mediator.Send(new CreateMenuCommand
        {
            Menu = menu,
            Image = image
        });

        return Ok(result);
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> Update([FromForm] Menu menu, IFormFile? image)
    {
        var result = await _mediator.Send(new UpdateMenuCommand
        {
            Menu = menu,
            Image = image
        });

        return Ok(result);
    }
}