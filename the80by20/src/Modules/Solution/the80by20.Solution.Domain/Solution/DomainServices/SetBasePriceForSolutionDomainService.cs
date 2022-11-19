using the80by20.Modules.Solution.Domain.Solution.Entities;
using the80by20.Modules.Solution.Domain.Solution.Factories;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Kernel.Capabilities;
using the80by20.Shared.Abstractions.Time;

namespace the80by20.Modules.Solution.Domain.Solution.DomainServices;

[DomainServiceDdd]
public sealed class SetBasePriceForSolutionDomainService
{
    public void SetBasePrice(SolutionToProblemAggregate solution, IClock clock)
    {
        int requiredSolutionTypesCount = solution.RequiredSolutionTypes.Elements.Count;

        var price = Money.FromValue(2000m * requiredSolutionTypesCount);

        int percentageDiscount = DiscountPolicyFactory.CreatePolicy(clock).PercentageDiscount();

        price = price.Percentage(percentageDiscount);

        solution.SetBasePrice(price);
    }
}
