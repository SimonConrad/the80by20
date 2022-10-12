using MediatR;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Solution.Domain.Operations.Solution;

namespace the80by20.Solution.App.Events.SolutionEvents;

[DomainEventDdd]
public sealed record StartedWorkingOnSolution(SolutionToProblemId SolutionToProblemId) : INotification;