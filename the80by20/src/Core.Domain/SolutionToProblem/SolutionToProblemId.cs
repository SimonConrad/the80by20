﻿using Common.TacticalDDD;

namespace Core.Domain.SolutionToProblem;

[ValueObjectDdd]
public class SolutionToProblemId
{
    public Guid Value { get;  init; }

    public static SolutionToProblemId FromGuid(Guid id) => new() { Value = id };
    public static SolutionToProblemId New() => new() { Value = Guid.NewGuid() };
}