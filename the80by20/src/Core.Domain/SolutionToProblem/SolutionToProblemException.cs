using Common.TacticalDDD;

namespace Core.Domain.SolutionToProblem;

[DomainExceptionDdd]
public class SolutionToProblemException : Exception
{
    public SolutionToProblemException(string msg) : base(msg)
    {
    }
}