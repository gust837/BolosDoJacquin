using System;
namespace BolosDoJacquin.Exceptions
{
    public class ConflictException : Exception { public ConflictException(string msg) : base(msg) {} }
    public class NotFoundException : Exception { public NotFoundException(string msg) : base(msg) {} }
}
