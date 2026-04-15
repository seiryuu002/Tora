namespace Tora.Domain.Exceptions;

public class ForbiddenException(string message) : DomainExceptions(message)
{
    public static ForbiddenException ForAction(string action) => new($"You do not have persmission to {action}."); 
}
