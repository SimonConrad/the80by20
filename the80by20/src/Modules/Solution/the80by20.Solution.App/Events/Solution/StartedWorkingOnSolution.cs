using MediatR;
using the80by20.Modules.Solution.Domain.Solution;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

namespace the80by20.Modules.Solution.App.Events.Solution;

[DomainEventDdd]
public sealed record StartedWorkingOnSolution(SolutionToProblemId SolutionToProblemId) : INotification;