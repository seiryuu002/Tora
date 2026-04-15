using System;

namespace Tora.Domain.Exceptions;

public class NotFoundException : DomainExceptions
{
    public NotFoundException(string message) : base(message){}
    public NotFoundException(string entityName, object id): base($"{entityName} with id {id} was not found"){}
}
