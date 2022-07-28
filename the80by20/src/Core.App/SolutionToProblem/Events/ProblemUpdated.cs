using Core.Domain.SolutionToProblem.Operations;
using MediatR;

namespace Core.App.SolutionToProblem.Events;

public sealed record ProblemUpdated(SolutionToProblemId SolutionToProblemId) : INotification;
