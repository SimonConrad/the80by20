using MediatR;
using the80by20.Domain.ArchitectureBuildingBlocks;
using the80by20.Domain.Core.SolutionToProblem.Operations.Problem;

namespace the80by20.App.Core.SolutionToProblem.Events;

[DomainEventDdd]
public sealed record ProblemCreated(ProblemId ProblemId) : INotification;


[DomainEventDdd]
public sealed record ProblemUpdated(ProblemId ProblemId) : INotification;