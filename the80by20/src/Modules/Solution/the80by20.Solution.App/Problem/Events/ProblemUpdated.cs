using MediatR;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Kernel.Types;

namespace the80by20.Modules.Solution.App.Problem.Events;

[DomainEventDdd]  // todo add domain event ad this remove
public sealed record ProblemUpdated(ProblemId ProblemId) : INotification;