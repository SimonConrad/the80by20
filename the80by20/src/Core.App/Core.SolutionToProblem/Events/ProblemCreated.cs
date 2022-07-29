using Common.DDD;
using Core.Domain.Core.SolutionToProblem.Operations;
using MediatR;

namespace Core.App.Core.SolutionToProblem.Events;

[DomainEventDdd]
public sealed record ProblemCreated(SolutionToProblemId SolutionToProblemId) : INotification;
