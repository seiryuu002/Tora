using System.ComponentModel.DataAnnotations;

namespace Tora.Domain.Entities;

public class Role
{
    public Guid Id { get; set; }
    public required string UserRole { get; set; }
}
