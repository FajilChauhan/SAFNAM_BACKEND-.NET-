using Dapper;
using Microsoft.AspNetCore.Mvc;
using SafnamBackend.Data;
using SafnamBackend.Models;

namespace SafnamBackend.Controllers
{
    [Route("api/roombooking")]
    [ApiController]
    public class RoomBookingController : ControllerBase
    {
        private readonly DapperContext _c;

        public RoomBookingController(DapperContext c)
        {
            _c = c;
        }

        // ✅ GET ALL BOOKINGS
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            using var con = _c.CreateConnection();
            var data = await con.QueryAsync<RoomBooking>("SELECT * FROM RoomBookings");
            return Ok(data);
        }

        // ✅ GET BOOKINGS BY ROOM (for conflict check)
        [HttpGet("room/{roomId}")]
        public async Task<IActionResult> GetBookingsByRoom(int roomId)
        {
            using var con = _c.CreateConnection();

            var bookings = await con.QueryAsync<RoomBooking>(
                "SELECT * FROM RoomBookings WHERE RoomId = @roomId",
                new { roomId });

            return Ok(bookings); // returns [] if none
        }

        // ✅ CREATE BOOKING
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoomBooking b)
        {
            using var con = _c.CreateConnection();

            // 🔹 Validate dates
            if (b.CheckOut <= b.CheckIn)
                return BadRequest("Check-out must be after check-in");

            // 🔹 calculate nights
            var nights = (b.CheckOut - b.CheckIn).Days;

            // 🔹 get price
            var price = await con.QueryFirstOrDefaultAsync<decimal>(
                "SELECT PricePerDay FROM Rooms WHERE Id=@id",
                new { id = b.RoomId });

            if (price == 0)
                return BadRequest("Invalid room");

            var totalAmount = nights * price;

            await con.ExecuteAsync(
            @"INSERT INTO RoomBookings
            (RoomId, UserId, CheckIn, CheckOut, Status, PaymentStatus, TotalAmount)
            VALUES
            (@RoomId, @UserId, @CheckIn, @CheckOut, 1, 'Pending', @TotalAmount)",
            new
            {
                b.RoomId,
                b.UserId,
                b.CheckIn,
                b.CheckOut,
                TotalAmount = totalAmount
            });

            return Ok(new
            {
                message = "Room booked successfully",
                totalAmount
            });
        }

        // ✅ UPDATE BOOKING
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RoomBooking b)
        {
            using var con = _c.CreateConnection();

            await con.ExecuteAsync(
            @"UPDATE RoomBookings
              SET RoomId=@RoomId,
                  UserId=@UserId,
                  CheckIn=@CheckIn,
                  CheckOut=@CheckOut,
                  Status=@Status,
                  PaymentStatus=@PaymentStatus,
                  TotalAmount=@TotalAmount
              WHERE Id=@id",
            new
            {
                b.RoomId,
                b.UserId,
                b.CheckIn,
                b.CheckOut,
                b.Status,
                b.PaymentStatus,
                b.TotalAmount,
                id
            });

            return Ok("Updated");
        }

        // ✅ DELETE BOOKING
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            using var con = _c.CreateConnection();
            await con.ExecuteAsync(
                "DELETE FROM RoomBookings WHERE Id=@id",
                new { id });

            return Ok("Deleted");
        }
    }
}