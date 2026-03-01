using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafnamBackend.Data;
using SafnamBackend.Models;

namespace SafnamBackend.Controllers;

[Route("api/order")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly DapperContext _c;

    public OrderController(DapperContext c)
    {
        _c = c;
    }

    // ================= CREATE ORDER =================
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreateOrderDto dto)
    {
        using var con = _c.CreateConnection();
        var userId = User.FindFirst("UserId")?.Value;
        decimal total = 0;

        foreach (var item in dto.Items)
        {
            var price = await con.ExecuteScalarAsync<decimal>(
                "SELECT Price FROM Menu WHERE Id=@id",
                new { id = item.MenuId });

            total += price * item.Quantity;
        }

        dto.Order.TotalAmount = total;
        dto.Order.PaymentStatus = "Pending";
        dto.Order.Status = "Pending";

        var orderId = await con.ExecuteScalarAsync<int>(
            @"INSERT INTO Orders(UserId,Address,TotalAmount,PaymentStatus,Status)
              OUTPUT INSERTED.Id
              VALUES(@UserId,@Address,@TotalAmount,@PaymentStatus,@Status)",
            dto.Order);

        foreach (var item in dto.Items)
        {
            item.OrderId = orderId;
            await con.ExecuteAsync(
                "INSERT INTO OrderItems(OrderId,MenuId,Quantity) VALUES(@OrderId,@MenuId,@Quantity)",
                item);
        }

        return Ok(new { orderId, total });
    }

    // ================= GET ALL ORDERS (ADMIN) =================
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        using var con = _c.CreateConnection();
        return Ok(await con.QueryAsync<Order>("SELECT * FROM Orders"));
    }

    // ================= GET ORDER BY ID =================
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        using var con = _c.CreateConnection();

        var order = await con.QueryFirstOrDefaultAsync<Order>(
            "SELECT * FROM Orders WHERE Id=@id", new { id });

        if (order == null) return NotFound();

        var items = await con.QueryAsync<OrderItem>(
            "SELECT * FROM OrderItems WHERE OrderId=@id", new { id });

        return Ok(new { order, items });
    }

    // ================= GET ORDERS BY USER =================
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUser(int userId)
    {
        using var con = _c.CreateConnection();

        var orders = await con.QueryAsync<Order>(
            "SELECT * FROM Orders WHERE UserId=@userId",
            new { userId });

        return Ok(orders);
    }

    // ================= UPDATE STATUS / PAYMENT =================
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Order o)
    {
        using var con = _c.CreateConnection();

        await con.ExecuteAsync(
            @"UPDATE Orders 
              SET PaymentStatus=@PaymentStatus, Status=@Status
              WHERE Id=@id",
            new { o.PaymentStatus, o.Status, id });

        return Ok("Updated");
    }

    // ================= DELETE ORDER =================
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        using var con = _c.CreateConnection();

        // delete items first
        await con.ExecuteAsync("DELETE FROM OrderItems WHERE OrderId=@id", new { id });

        // delete order
        await con.ExecuteAsync("DELETE FROM Orders WHERE Id=@id", new { id });

        return Ok("Deleted");
    }
}
