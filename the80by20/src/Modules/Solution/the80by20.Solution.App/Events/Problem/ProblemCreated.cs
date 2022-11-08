using MediatR;
using the80by20.Modules.Solution.Domain.Problem;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

namespace the80by20.Modules.Solution.App.Events.Problem;

[DomainEventDdd]
public sealed record ProblemCreated(ProblemId ProblemId) : INotification;