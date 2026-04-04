using Tora.Domain.Entities;

namespace Tora.Application.Interfaces;

public interface IJwtService
{
    string GenerateToken(User user);
}
