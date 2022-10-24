using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.DomainLayer.SharedKernel.Capabilities;
using the80by20.Shared.Abstractions.Time;
using the80by20.Solution.Domain.Operations.Solution;

namespace the80by20.Solution.Domain.Operations.DomainServices;

[DomainServiceDdd]
public sealed class SetBasePriceForSolutionToProblemDomainService
{
    private readonly IClock _clock; // whentesting register own clock in ioc for tests

    public SetBasePriceForSolutionToProblemDomainService(IClock clock)
    {
        _clock = clock;
    }

    public void SetBasePrice(SolutionToProblemAggregate solution)
    {
        int requimentsolutiontypescount = solution.RequiredSolutionTypes.Elements.Count;

        var price = Money.FromValue(2000m * requimentsolutiontypescount);

        int percentageDiscount = CreatePolicy().PercentageDiscount();

        price = price.Percentage(percentageDiscount);

        solution.SetBasePrice(price);
    }

    [FactoryDdd]
    public IDiscountPolicy CreatePolicy()
    {
        var month = _clock.Current().Month;

        return month switch
        {
            4 => new Percents20Policy(),
            9 => new Percents10Policy(),
            _ => new NoPercentsPolicy()
        };
    }
}