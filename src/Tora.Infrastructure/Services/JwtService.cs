
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Tora.Infrastructure.Interfaces;
using Tora.Domain.Entities;

namespace Tora.Infrastructure.Services;

public class JwtService(IConfiguration config) : IJwtService
{
    private readonly IConfiguration _config = config;

    public string GenerateToken(User user)
    {
        // Implemented JWT token generation logic here
        // This typically involves creating claims based on the user's information,
        // signing the token with a secret key, and returning the token as a string.

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.RoleId.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var keyString = _config["Jwt:Key"]?? throw new Exception("Jwt Key not found in configuration");
        var issuer = _config["Jwt:Issuer"]?? throw new Exception("Jwt Issuer not found in the configuration");
        var audience = _config["Jwt:Audience"]?? throw new Exception("Jwt Audience not found in the configuration");
        var expiryHours = _config["Jwt:ExpiryHours"]?? throw new Exception("Jwt ExpiryHours not found in the configuration");

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(keyString));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(int.Parse(expiryHours)),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}
