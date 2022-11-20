using the80by20.Shared.Abstractions.Events;

namespace the80by20.Modules.Solution.App.Solution.Events;

public record SolutionArchived(Guid solutionId) : IEvent;