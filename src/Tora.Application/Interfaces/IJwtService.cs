namespace Tora.Application.Interfaces;

public interface IJwtService
{
    string GenerateToken(string id, string email, string role);
}

