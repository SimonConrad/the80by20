using MediatR;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Solution.Domain.SolutionToProblem.Operations.Problem;

namespace the80by20.Solution.App.SolutionToProblem.Events.ProblemEvents;

[DomainEventDdd]
public sealed record ProblemCreated(ProblemId ProblemId) : INotification;