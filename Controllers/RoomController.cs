using Dapper;
using Microsoft.AspNetCore.Mvc;
using SafnamBackend.Data;
using SafnamBackend.Models;

[Route("api/room")]
[ApiController]
public class RoomController : ControllerBase
{
    private readonly DapperContext _c;
    private readonly IWebHostEnvironment _env;

    public RoomController(DapperContext c, IWebHostEnvironment env)
    {
        _c = c;
        _env = env;
    }

    // ✅ GET ROOMS
    [HttpGet]
    public async Task<IActionResult> GetRooms()
    {
        using var con = _c.CreateConnection();
        return Ok(await con.QueryAsync<Room>("SELECT * FROM Rooms"));
    }

    // ✅ ADD ROOM WITH IMAGE
    [HttpPost]
    public async Task<IActionResult> Add([FromForm] int RoomNo,
                                            [FromForm] string Type,
                                            [FromForm] decimal PricePerDay, 
                                            IFormFile image)
    {
        if (image == null)
            return BadRequest("Image required");

        var folder = Path.Combine(_env.WebRootPath, "images");
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        var fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
        var path = Path.Combine(folder, fileName);

        using var stream = new FileStream(path, FileMode.Create);
        await image.CopyToAsync(stream);

        var imagePath = "/images/" + fileName;

        using var con = _c.CreateConnection();
        await con.ExecuteAsync(
            @"INSERT INTO Rooms(RoomNo, Type, ImagePath, PricePerDay)
              VALUES(@RoomNo, @Type, @ImagePath, @PricePerDay)",
            new
            {
                RoomNo,
                Type,
                ImagePath = imagePath,
                PricePerDay
            });

        return Ok("Room added");
    }

    // ✅ UPDATE ROOM (OPTIONAL IMAGE)
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id,  IFormFile? image, 
                                            [FromForm] int RoomNo,
                                            [FromForm] string Type,
                                            [FromForm] decimal PricePerDay)
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

        using var con = _c.CreateConnection();

        if (imagePath == null)
        {
            await con.ExecuteAsync(
                @"UPDATE Rooms 
                  SET RoomNo=@RoomNo, Type=@Type, PricePerDay=@PricePerDay 
                  WHERE Id=@id",
                new { RoomNo, Type, PricePerDay, id });
        }
        else
        {
            await con.ExecuteAsync(
                @"UPDATE Rooms 
                  SET RoomNo=@RoomNo, Type=@Type, PricePerDay=@PricePerDay, ImagePath=@ImagePath 
                  WHERE Id=@id",
                new { RoomNo, Type, PricePerDay, ImagePath = imagePath, id });
        }

        return Ok("Room updated");
    }

    // ✅ DELETE ROOM
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        using var con = _c.CreateConnection();
        await con.ExecuteAsync("DELETE FROM Rooms WHERE Id=@id", new { id });
        return Ok("Deleted");
    }
}