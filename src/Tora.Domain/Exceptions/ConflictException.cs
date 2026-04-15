namespace Tora.Domain.Exceptions;

// throw this exception when a conflict happens 
// for eg. if email id is alreay in use 
// Maps to HTTP Status code 409 -> conflict
public class ConflictException(string message) : DomainExceptions(message){}
