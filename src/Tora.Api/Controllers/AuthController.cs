using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tora.Application.DTOs.Auth;
using Tora.Application.Interfaces;
using Tora.Domain.Entities;
using Tora.Persistence;

namespace Tora.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(ToraDbContext context, IJwtService jwtService) : ControllerBase
    {
        private readonly ToraDbContext _context = context;
        private readonly IJwtService _jwtService = jwtService;

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Name = registerDto.Name,
                Role = _context.Roles.First(r => r.UserRole == "User"),
                RoleId = _context.Roles.First(r => r.UserRole == "User").Id,
                Email = registerDto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                return Unauthorized("Invalid email or password");
            }

            var token = _jwtService.GenerateToken(user);
            return Ok(new { Token = token });
        }
    }
}



