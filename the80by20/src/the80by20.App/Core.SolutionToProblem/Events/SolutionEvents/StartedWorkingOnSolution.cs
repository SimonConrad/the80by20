using MediatR;
using the80by20.Common.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Domain.Core.SolutionToProblem.Operations.Solution;

namespace the80by20.App.Core.SolutionToProblem.Events.SolutionEvents;

[DomainEventDdd]
public sealed record StartedWorkingOnSolution(SolutionToProblemId SolutionToProblemId) : INotification;