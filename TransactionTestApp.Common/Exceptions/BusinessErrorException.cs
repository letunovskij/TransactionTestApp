using TransactionTestApp.Common.Responses;

namespace TransactionTestApp.Common.Exceptions;

public sealed class BusinessErrorException : Exception
{
    public ProblemDetails? ProblemDetails { get; set; }

    public BusinessErrorException(ProblemDetails details) { ProblemDetails = details; }

    public BusinessErrorException() { }

    public BusinessErrorException(string message) : base(message) { }

    public BusinessErrorException(string message, Exception innerException) : base(message, innerException) { }
}
