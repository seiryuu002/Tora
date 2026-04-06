using Tora.Application.Interfaces;

namespace Tora.Infrastructure.Services;

public class HashingService : IHashingService
{
    public string Hash(string Password) => BCrypt.Net.BCrypt.HashPassword(Password);
}
