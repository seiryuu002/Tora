using System.ComponentModel.DataAnnotations;

namespace Tora.Domain.Entities;

public class Comment
{
    public Guid Id { get; set; }
    public required string Content { get; set; }
    public Guid TaskId { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }

}
