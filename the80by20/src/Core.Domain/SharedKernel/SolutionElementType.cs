using Common.TacticalDDD;

namespace Core.Domain.SharedKernel;

[DomainEnumDdd]
public enum SolutionElementType
{
    RoiAnalysis,
    TheoryOfConceptWithExample,
    PlanOfImplmentingChnageInCode,
    PocInCode
}