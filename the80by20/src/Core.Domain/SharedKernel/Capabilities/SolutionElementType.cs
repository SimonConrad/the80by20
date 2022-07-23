using Common.DDD;

namespace Core.Domain.SharedKernel.Capabilities;

[DomainEnumDdd]
public enum SolutionElementType
{
    RoiAnalysis,
    TheoryOfConceptWithExample,
    PlanOfImplmentingChnageInCode,
    PocInCode
}