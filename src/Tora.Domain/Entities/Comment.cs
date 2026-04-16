 
// To Do update entity with factory methods 

namespace Tora.Domain.Entities;

public class Comment
{
    public Guid Id { get; private set; }
    public string Content { get; private set; } = default!;
    public Guid TaskId { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime CreatedAt { get; private set; }

}
