using MediatR;
using the80by20.Domain.ArchitectureBuildingBlocks;
using the80by20.Domain.Core.SolutionToProblem.Operations.Solution;

namespace the80by20.App.Core.SolutionToProblem.Events;

[DomainEventDdd]
public sealed record StartedWorkingOnSolutionToProblem(SolutionToProblemId SolutionToProblemId) : INotification;