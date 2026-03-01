using Microsoft.AspNetCore.Mvc;
using SafnamBackend.Data;
using Dapper;
using SafnamBackend.Models;

namespace SafnamBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MenuController : ControllerBase
{
    private readonly DapperContext _context;
    private readonly IWebHostEnvironment _env;

    public MenuController(DapperContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    // GET ALL
    [HttpGet]
    public async Task<IActionResult> GetMenu()
    {
        var sql = "SELECT * FROM Menu";
        using var connection = _context.CreateConnection();
        var data = await connection.QueryAsync<Menu>(sql);
        return Ok(data);
    }

    // ADD
    [HttpPost]
    public async Task<IActionResult> AddMenu([FromForm] string itemName,
                                            [FromForm] decimal price,
                                            [FromForm] bool isAvailable,
                                            [FromForm] string type,
                                            IFormFile image)
    {
        if (image == null) return BadRequest("Image required");

        var folder = Path.Combine(_env.WebRootPath, "images");
        if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

        var fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
        var path = Path.Combine(folder, fileName);

        using var stream = new FileStream(path, FileMode.Create);
        await image.CopyToAsync(stream);

        var imagePath = "/images/" + fileName;

        var sql = @"INSERT INTO Menu(ItemName,ImagePath,Price,Type,IsAvailable)
              VALUES(@itemName,@imagePath,@price,@type,@isAvailable)";

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(sql, new { itemName, imagePath, price, type, isAvailable });

        return Ok("Menu added");
    }

    // UPDATE
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMenu(int id, [FromForm] string itemName,
                                                [FromForm] decimal price,
                                                [FromForm] string type,
                                                [FromForm] bool isAvailable,
                                                IFormFile? image)
    {
        string imagePath = null;

        if (image != null)
        {
            var folder = Path.Combine(_env.WebRootPath, "images");
            var fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
            var path = Path.Combine(folder, fileName);

            using var stream = new FileStream(path, FileMode.Create);
            await image.CopyToAsync(stream);

            imagePath = "/images/" + fileName;
        }

        var sql = image == null ?
        @"UPDATE Menu SET ItemName=@itemName,Price=@price,Type=@type,IsAvailable=@isAvailable WHERE Id=@id" :
        @"UPDATE Menu SET ItemName=@itemName,ImagePath=@imagePath,Price=@price,Type=@type,IsAvailable=@isAvailable WHERE Id=@id";

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(sql, new { id, itemName, imagePath, price, type, isAvailable });

        return Ok("Updated");
    }

    // DELETE
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMenu(int id)
    {
        var sql = "DELETE FROM Menu WHERE Id=@id";
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(sql, new { id });
        return Ok("Deleted");
    }
}
