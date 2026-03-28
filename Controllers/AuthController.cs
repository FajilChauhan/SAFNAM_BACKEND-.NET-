using BCrypt.Net;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Generators;
using SafnamBackend.Application.DTO;
using SafnamBackend.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace SafnamBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public AuthController(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            using var db = new SqlConnection(_connectionString);

            // check user exists
            var exists = db.QueryFirstOrDefault<int>(
                "SELECT COUNT(1) FROM Users WHERE Username = @Username",
                new { request.Username });

            if (exists > 0)
                return BadRequest("Username already exists");

            var password = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var sql = @"
            INSERT INTO Users (Username, Phone, Role, Password, CreatedAt)
            VALUES (@Username, @Phone, 'User', @Password, GETDATE())
        ";

            db.Execute(sql, new
            {
                request.Username,
                request.Phone,
                Password = password
            });

            return Ok("User registered successfully");
        }

        // ---------------- LOGIN ----------------
        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            using var db = new SqlConnection(_connectionString);

            var user = db.QueryFirstOrDefault<dynamic>(
                "SELECT * FROM Users WHERE Username = @Username",
                new { request.Username });

            if (user == null)
                return Unauthorized("Invalid username or password");

            bool validPassword = BCrypt.Net.BCrypt.Verify(
                request.Password, user.Password);

            if (!validPassword)
                return Unauthorized("Invalid username or password");

            // JWT claims
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("UserId", user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"])
            );

            var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(
                Convert.ToDouble(_config["Jwt:ExpiryMinutes"])
            ),
            signingCredentials: new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256)
        );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                username = user.Username,
                role = user.Role
            });
        }
    }
}
