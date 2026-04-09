
namespace Tora.Domain.Entities;

public class Task()
{
    public Guid Id { get; private set; }
    public string? Title { get; private set; }
    public string? Description { get; private set; }
    public Enums.TaskStatus Status { get; private set;} = Enums.TaskStatus.NotStarted;
    public Enums.TaskPriority Priority { get; private set; }
    public Guid ProjectId { get; private set;}
    public Guid AssignedtoUserId { get; set;}
    public DateTime DueDate { get; set; }

}
