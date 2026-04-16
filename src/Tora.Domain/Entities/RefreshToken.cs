using System.Data.Common;

namespace Tora.Domain.Entities;

public class RefreshToken
{
    public Guid Id {get; private set;}
    public string Token {get; private set;} = default!;
    public string TokenPrefix {get; private set;} = default!;
    public Guid UserId{get; private set;}
    public DateTime ExpiryDate {get; private set;}
    public bool IsRevoked {get; private set;}
    public User User {get; private set;} = default!;

    private RefreshToken() {}

    public static RefreshToken Create(string hashedToken, User user, int expiryDays = 7)
    {
        return new RefreshToken
        {
            Id = Guid.NewGuid(),
            Token = hashedToken,
            TokenPrefix = hashedToken[..8],
            UserId = user.Id,
            ExpiryDate = DateTime.UtcNow.AddDays(expiryDays),
            IsRevoked = false,
            User = user
        };
    }

    public void Revoke()
    {
        IsRevoked = true;
    }
}
