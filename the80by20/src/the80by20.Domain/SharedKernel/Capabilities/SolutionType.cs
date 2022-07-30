﻿using the80by20.Domain.ArchitectureBuildingBlocks;

namespace the80by20.Domain.SharedKernel.Capabilities;

[DomainEnumDdd]
public enum SolutionType
{
    TheoryOfConceptWithExample,
    RoiAnalysis,
    PlanOfImplmentingChangeInCode,
    PocInCode,
    // TODO Consulting with mentor; such element type needs separate module for managing calendar meetings with mentors + theirs availability
}