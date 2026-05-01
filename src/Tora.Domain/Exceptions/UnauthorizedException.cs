using System;

namespace Tora.Domain.Exceptions;

public class UnauthorizedException(string message) : DomainExceptions(message){}