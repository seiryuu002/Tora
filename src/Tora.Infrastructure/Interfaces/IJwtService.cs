using Tora.Domain.Entities;

namespace Tora.Infrastructure.Interfaces;

public interface IJwtService
{
    string GenerateToken(string Id, string email, string role);
}

