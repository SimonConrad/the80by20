using Common.DDD;
using Core.Domain.SharedKernel;

namespace Core.Domain.SolutionToProblem.Operations;

[DomainExceptionDdd]
public class SolutionToProblemException : CustomException
{
    public SolutionToProblemException(string msg) : base(msg)
    {
    }
}