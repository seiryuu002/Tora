using System.Diagnostics.CodeAnalysis;

namespace Tora.Domain.Entities;

public class Task
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string Status { get; set;}
    public required string Priority { get; set; }
    public Guid ProjectId { get; set;}
    public Guid AssignedtoUserId { get; set;}
    public DateTime DueDate { get; set; }
}
