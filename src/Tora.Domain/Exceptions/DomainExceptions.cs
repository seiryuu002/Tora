namespace Tora.Domain.Exceptions;

// Base class for exception it is not meant to be instantiated hence it is marked abstract, it is for inheritance only 
public abstract class DomainExceptions(string message) : Exception(message){}