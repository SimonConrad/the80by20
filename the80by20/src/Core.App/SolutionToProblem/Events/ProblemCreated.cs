using Common.DDD;
using Core.Domain.SolutionToProblem.Operations;
using MediatR;

namespace Core.App.SolutionToProblem.Events;

[DomainEventDdd]
public sealed record ProblemCreated(SolutionToProblemId SolutionToProblemId) : INotification;
