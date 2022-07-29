using Core.Domain.Core.SolutionToProblem.Operations;
using MediatR;

namespace Core.App.Core.SolutionToProblem.Events;

public sealed record ProblemUpdated(SolutionToProblemId SolutionToProblemId) : INotification;
