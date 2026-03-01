using Microsoft.AspNetCore.Mvc;
using SafnamBackend.Data;
using Dapper;
using SafnamBackend.Models;

namespace SafnamBackend.Controllers;

[Route("api/tablebooking")]
[ApiController]
public class TableBookingController : ControllerBase
{
    private readonly DapperContext _c;
    public TableBookingController(DapperContext c) { _c = c; }

    // GET ALL BOOKINGS
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        using var con = _c.CreateConnection();
        return Ok(await con.QueryAsync<TableBooking>("SELECT * FROM TableBookings"));
    }

    // GET BY ID
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        using var con = _c.CreateConnection();
        var b = await con.QueryFirstOrDefaultAsync<TableBooking>(
            "SELECT * FROM TableBookings WHERE Id=@id", new { id });

        if (b == null) return NotFound();
        return Ok(b);
    }

    // CREATE
    [HttpPost]
    public async Task<IActionResult> Create(TableBooking b)
    {
        using var con = _c.CreateConnection();

        await con.ExecuteAsync(
            "INSERT INTO TableBookings(TableId,UserId,BookingDate,TimeSlot,Status) VALUES(@TableId,@UserId,@BookingDate,@TimeSlot,1)",
            b);

        return Ok("Booked");
    }

    // UPDATE
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TableBooking b)
    {
        using var con = _c.CreateConnection();

        await con.ExecuteAsync(
            @"UPDATE TableBookings 
              SET TableId=@TableId,UserId=@UserId,BookingDate=@BookingDate,
                  TimeSlot=@TimeSlot,Status=@Status
              WHERE Id=@id",
            new { b.TableId, b.UserId, b.BookingDate, b.TimeSlot, b.Status, id });

        return Ok("Updated");
    }

    // DELETE
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        using var con = _c.CreateConnection();
        await con.ExecuteAsync("DELETE FROM TableBookings WHERE Id=@id", new { id });
        return Ok("Deleted");
    }
}
