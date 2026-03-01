using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SafnamBackend.Data;
using SafnamBackend.Models;

namespace SafnamBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantTablesController : ControllerBase
    {
        private readonly DapperContext _context;

        public RestaurantTablesController(DapperContext context)
        {
            _context = context;
        }

        // GET ALL TABLES
        [HttpGet]
        public async Task<IActionResult> GetTables()
        {
            using var con = _context.CreateConnection();
            var data = await con.QueryAsync<RestaurantTable>("SELECT * FROM RestaurantTables");
            return Ok(data);
        }

        // GET SINGLE TABLE
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTable(int id)
        {
            using var con = _context.CreateConnection();
            var table = await con.QueryFirstOrDefaultAsync<RestaurantTable>(
                "SELECT * FROM RestaurantTables WHERE Id=@id", new { id });

            if (table == null) return NotFound();

            return Ok(table);
        }

        // ADD TABLE (Admin)
        [HttpPost]
        public async Task<IActionResult> AddTable(RestaurantTable t)
        {
            using var con = _context.CreateConnection();

            await con.ExecuteAsync(
                "INSERT INTO RestaurantTables(TableNo,Floor,ExtraCharge) VALUES(@TableNo,@Floor,@ExtraCharge)",
                t);

            return Ok("Table Added");
        }

        // UPDATE TABLE
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTable(int id, RestaurantTable t)
        {
            using var con = _context.CreateConnection();

            await con.ExecuteAsync(
                @"UPDATE RestaurantTables 
              SET TableNo=@TableNo,Floor=@Floor,ExtraCharge=@ExtraCharge
              WHERE Id=@id",
                new { t.TableNo, t.Floor, t.ExtraCharge, id });

            return Ok("Updated");
        }

        // DELETE TABLE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTable(int id)
        {
            using var con = _context.CreateConnection();

            await con.ExecuteAsync(
                "DELETE FROM RestaurantTables WHERE Id=@id",
                new { id });

            return Ok("Deleted");
        }
    }
}
