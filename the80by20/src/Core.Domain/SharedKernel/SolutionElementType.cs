using Common.TacticalDDD;

namespace Core.Domain.SharedKernel;

[DomainEnumDDD]
public enum SolutionElementType
{
    RoiAnalysis,
    TheoryOfConceptWithExample,
    PlanOfImplmentingChnageInCode,
    PocInCode
}