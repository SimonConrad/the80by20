﻿using the80by20.Modules.Solution.Domain.Solution.Entities;
using the80by20.Shared.Abstractions.Events;
using the80by20.Shared.Abstractions.Kernel.Types;

namespace the80by20.Modules.Solution.App.Solution.Events;


public sealed record StartedWorkingOnSolution(SolutionToProblemAggregate solution) : IEvent;