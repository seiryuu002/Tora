
// To Do update entity with factory methods

namespace Tora.Domain.Entities;

public class Project
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public Guid OwnerUserId { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }

}
