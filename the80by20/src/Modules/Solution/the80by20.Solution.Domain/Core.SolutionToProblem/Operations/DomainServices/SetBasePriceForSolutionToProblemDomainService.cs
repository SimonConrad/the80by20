using the80by20.Domain.Core.SolutionToProblem.Operations.Solution;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.DomainLayer.SharedKernel;
using the80by20.Shared.Abstractions.DomainLayer.SharedKernel.Capabilities;

namespace the80by20.Domain.Core.SolutionToProblem.Operations.DomainServices;

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

        int percentageDiscount =  CreatePolicy().PercentageDiscount();

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