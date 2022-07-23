using Common.DDD;

namespace Core.Domain.SolutionToProblem.Operations;

[DomainExceptionDdd]
public class SolutionToProblemException : Exception
{
    public SolutionToProblemException(string msg) : base(msg)
    {
    }
}