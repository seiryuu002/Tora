using Tora.Domain.Entities;
namespace Tora.Application.Interfaces;

public interface IJwtService
{
    public string GenerateAccessToken(string name, string email, string role);
    public string GenerateRefreshToken();
}

