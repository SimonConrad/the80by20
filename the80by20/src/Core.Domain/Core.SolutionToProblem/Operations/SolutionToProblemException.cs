using Common.DDD;

namespace Core.Domain.Core.SolutionToProblem.Operations;

[DomainExceptionDdd]
public class SolutionToProblemException : CustomException
{
    public SolutionToProblemException(string msg) : base(msg)
    {
    }
}