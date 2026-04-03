using System.Diagnostics.CodeAnalysis;

namespace Tora.Domain.Entities;

public class Task
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string Status { get; set;}
    public required string Priority { get; set; }
    public int ProjectId { get; set;}
    public int AssignedtoUserId { get; set;}
    public DateTime DueDate { get; set; }
}
