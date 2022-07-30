using MediatR;
using the80by20.Domain.ArchitectureBuildingBlocks;
using the80by20.Domain.Core.SolutionToProblem.Operations.Solution;

namespace the80by20.App.Core.SolutionToProblem.Events;

[DomainEventDdd]
public sealed record StartedWorkingOnSolution(SolutionToProblemId SolutionToProblemId) : INotification;

[DomainEventDdd]
public sealed record UpdatedSolution(SolutionToProblemId SolutionToProblemId) : INotification;