using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

namespace the80by20.Modules.Solution.Domain.Shared;

[DomainEnumDdd]
public enum SolutionType
{
    TheoryOfConceptWithExample,
    RoiAnalysis,
    PlanOfImplmentingChangeInCode,
    PocInCode,
    // TODO Consulting with mentor; such element type needs separate module for managing calendar meetings with mentors + theirs availability
}