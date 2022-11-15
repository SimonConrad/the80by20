﻿using the80by20.Shared.Abstractions.Events;
using the80by20.Shared.Abstractions.Kernel.Types;

namespace the80by20.Modules.Solution.Domain.Problem.Events
{
    public record ProblemUpdated(ProblemId problemId) : IEvent;
}