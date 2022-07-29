using MediatR;
using the80by20.Domain.Core.SolutionToProblem.Operations;

namespace the80by20.App.Core.SolutionToProblem.Events;

public sealed record ProblemUpdated(SolutionToProblemId SolutionToProblemId) : INotification;
