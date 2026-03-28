using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafnamBackend.Application.Module.Order.Command;
using SafnamBackend.Application.Module.Order.Query;
using SafnamBackend.Domain.Models;

[Route("api/order")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // ================= CREATE =================
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
    {
        var userId = int.Parse(User.FindFirst("UserId")!.Value);

        // 🔥 set userId inside order
        command.Order.UserId = userId;

        var result = await _mediator.Send(command);

        return Ok(result);
    }

    // ================= GET ALL =================
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _mediator.Send(new GetAllOrdersQuery()));
    }

    // ================= GET BY ID =================
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _mediator.Send(new GetOrderByIdQuery { Id = id });

        if (result == null) return NotFound();

        return Ok(result);
    }

    // ================= GET BY USER =================
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUser(int userId)
    {
        return Ok(await _mediator.Send(new GetOrdersByUserIdQuery
        {
            UserId = userId
        }));
    }

    // ================= GET PENDING =================
    [HttpGet("pending")]
    public async Task<IActionResult> GetPending()
    {
        return Ok(await _mediator.Send(new GetPendingOrdersQuery()));
    }

    // ================= UPDATE =================
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Order order)
    {
        order.Id = id; // 🔥 important

        var result = await _mediator.Send(new UpdateOrderCommand
        {
            Order = order
        });

        return Ok(result);
    }

    // ================= DELETE =================
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        // if needed you can convert to command later
        return Ok("Deleted");
    }
}