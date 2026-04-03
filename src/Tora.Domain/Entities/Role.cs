using System.ComponentModel.DataAnnotations;

namespace Tora.Domain.Entities;

public class Role
{
    public int Id { get; set; }
    public required string UserRole { get; set; }
}
