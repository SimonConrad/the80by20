using MediatR;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

namespace the80by20.Solution.App.SolutionToProblem.Events.SolutionEvents;

[DomainEventDdd]
public sealed record UpdatedSolution(SolutionToProblemId SolutionToProblemId) : INotification;