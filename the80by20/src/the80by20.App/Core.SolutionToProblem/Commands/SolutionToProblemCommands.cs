using MediatR;
using the80by20.Domain.ArchitectureBuildingBlocks;
using the80by20.Domain.Core.SolutionToProblem.Operations.Problem;
using the80by20.Domain.Core.SolutionToProblem.Operations.Solution;

namespace the80by20.App.Core.SolutionToProblem.Commands;

[CommandDdd]
public sealed record StartWorkingOnSolutionCommand(ProblemId ProblemId) : IRequest<SolutionToProblemId>;

[CommandDdd]
public sealed record SetBasePriceOfSolutionCommand(SolutionToProblemId SolutionToProblemId) : IRequest<SolutionToProblemId>;