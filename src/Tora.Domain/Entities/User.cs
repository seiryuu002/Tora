namespace Tora.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public string Password { get; private set; } = default!;
    public Guid RoleId {get; private set;}
    public Role? Role { get; private set; }

    private User(){} // for EF Core

    public static User Create(string name, string email, string hashedPassword, Guid roleId)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            Email = email.Trim(),
            Password = hashedPassword,
            RoleId = roleId
        };
    }

    public void AssignRole(Guid roleId)
    {
        RoleId = roleId;
    }

}
