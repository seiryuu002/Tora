namespace Tora.Application.Interfaces;

public interface IHashingService
{
    public string Hash(string Password);
    public bool Verify(string password, string hash);
}
