﻿using the80by20.App.MasterData.CategoryCrud;
using the80by20.Domain.Core.SolutionToProblem.Operations.Problem;
using the80by20.Domain.Core.SolutionToProblem.Operations.Solution;
using the80by20.Domain.SharedKernel.Capabilities;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

namespace the80by20.App.Core.SolutionToProblem.ReadModel;

// port
// INFO port in hexagon arch, its adapter in dal, IoC - so that app layer do not relay on dal
[Port]
[ReadModelDdd]
public interface ISolutionToProblemReadModelQueries
{
    Task<Category[]> GetProblemsCategories();

    IEnumerable<SolutionType> GetSolutionElementTypes();

    // todo create feature switch which disbales readmodel handler and then this query will return projections of
    // data retrieved straight from aggragtes data-sources and master-data data-sources 
    Task<SolutionToProblemReadModel> GetBySolutionId(SolutionToProblemId id);

    // todo create feature switch which disbales readmodel handler and then this query will return projections of
    // data retrieved straight from aggragtes data-sources and master-data data-sources 
    Task<SolutionToProblemReadModel> GetByProblemId(ProblemId id);

    Task<SolutionToProblemReadModel[]> GetByUserId(Guid userId);
}