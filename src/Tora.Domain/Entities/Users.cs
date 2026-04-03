using System.ComponentModel.DataAnnotations;

namespace Tora.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public Role? Role { get; set; }
    public int RoleId {get; set;}
    public required string Email { get; set; }
    public required string Password { get; set; }

}
