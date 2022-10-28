﻿using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.Exceptions;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

namespace the80by20.Solution.Domain.Operations.Solution;

[DomainExceptionDdd]
public class SolutionToProblemException : The80by20Exception
{
    public SolutionToProblemException(string msg) : base(msg)
    {
    }
}