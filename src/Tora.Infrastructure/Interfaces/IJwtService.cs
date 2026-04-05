using Tora.Domain.Entities;

namespace Tora.Infrastructure.Interfaces;

public interface IJwtService
{
    string GenerateToken(User user);
}

