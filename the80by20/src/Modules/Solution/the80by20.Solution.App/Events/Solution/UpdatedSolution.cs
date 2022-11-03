using MediatR;
using the80by20.Modules.Solution.Domain.Operations.Solution;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

namespace the80by20.Modules.Solution.App.Events.Solution;

[DomainEventDdd]
public sealed record UpdatedSolution(SolutionToProblemId SolutionToProblemId) : INotification;