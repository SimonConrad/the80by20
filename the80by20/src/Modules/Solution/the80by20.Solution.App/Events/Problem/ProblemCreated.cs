using MediatR;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Solution.Domain.Operations.Problem;

namespace the80by20.Solution.App.Events.Problem;

[DomainEventDdd]
public sealed record ProblemCreated(ProblemId ProblemId) : INotification;