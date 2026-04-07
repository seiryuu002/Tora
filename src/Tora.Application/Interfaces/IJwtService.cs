using Tora.Domain.Entities;
namespace Tora.Application.Interfaces;

public interface IJwtService
{
    public string GenerateAccessToken(string Id, string email, string role);
    public string GenerateRefreshToken();
}

