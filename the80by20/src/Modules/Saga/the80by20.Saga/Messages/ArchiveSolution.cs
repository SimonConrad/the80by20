using the80by20.Shared.Abstractions.Commands;

namespace the80by20.Saga.Messages;

internal record ArchiveSolution(Guid SolutionId) : ICommand;