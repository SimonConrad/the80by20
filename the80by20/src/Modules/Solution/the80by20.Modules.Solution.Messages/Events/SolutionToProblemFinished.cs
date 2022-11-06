﻿using the80by20.Shared.Abstractions.Events;

namespace the80by20.Modules.Solution.Messages.Events
{
    // INFO
    // seems that good idea is not to sent in paload only id
    // rationale - if id is only sent then module sale will need to synchronously get more solution detials from module solution, that will be temporal coupling
    // so the payload may include informations to operate in sale module without the need to retrieve data from module solution
    //
    // scope of information that is needed is modeled in event storming
    // solution becomes a product to sell, so it may have same id (snowflake idea from master-of-aggreagtes) and include ingofs like description, price, solution elements, ....
    // avoid good object / tabel which is one entity / table that implements model of 3 separate buisness concepts (spearate data and behavior and separate lifecycles)
    // so we avoid one entity / table taht has all data and methods for problem / solution / sale

    // process flow of what happens in the SolutionToProblemFinished handler is also modelled in es, bacouse this is process logic / application logic based upon buisness prcess
    public record SolutionToProblemFinished(
        Guid solutionId,
        Guid userThatRequestedSolution,
        string SolutionSummary, 
        string SolutionElementsGDriveLink, 
        decimal price) : IEvent; 
        
}