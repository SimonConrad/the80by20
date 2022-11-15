﻿using the80by20.Shared.Abstractions.Events;
using the80by20.Shared.Abstractions.Kernel.Types;

namespace the80by20.Modules.Solution.App.Solution.Events;


public sealed record UpdatedSolution(SolutionToProblemId SolutionToProblemId) : IEvent;