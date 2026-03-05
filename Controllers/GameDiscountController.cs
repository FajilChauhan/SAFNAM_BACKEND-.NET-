using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SafnamBackend.Data;
using SafnamBackend.Models;

namespace SafnamBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameDiscountController : ControllerBase
    {
        private readonly DapperContext _c;

        public GameDiscountController(DapperContext c)
        {
            _c = c;
        }

        // ================= GET USER DISCOUNT =================
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            using var con = _c.CreateConnection();

            var result = await con.QueryFirstOrDefaultAsync<GameDiscount>(
                @"SELECT TOP 1 * 
                  FROM GameDiscounts 
                  WHERE UserId = @userId AND Applied = 0
                  ORDER BY Date DESC",
                new { userId });

            return Ok(result);
        }

        // ================= SAVE GAME RESULT =================
        [HttpPost]
        public async Task<IActionResult> Create(GameDiscount g)
        {
            using var con = _c.CreateConnection();

            await con.ExecuteAsync(
                @"INSERT INTO GameDiscounts
                (UserId, GamePoint, Discount, Applied)
                VALUES
                (@UserId, @GamePoint, @Discount, 0)",
                g);

            return Ok("Game discount saved");
        }

        // ================= DELETE =================
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            using var con = _c.CreateConnection();

            await con.ExecuteAsync(
                "DELETE FROM GameDiscounts WHERE Id = @id",
                new { id });

            return Ok("Deleted");
        }
    }
}
