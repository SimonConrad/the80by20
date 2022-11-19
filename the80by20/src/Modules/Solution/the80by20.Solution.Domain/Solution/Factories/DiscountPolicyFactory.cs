using the80by20.Modules.Solution.Domain.Solution.Policies;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Time;

namespace the80by20.Modules.Solution.Domain.Solution.Factories;

[FactoryDdd]
public static class DiscountPolicyFactory
{
    public static IDiscountPolicy CreatePolicy(IClock clock)
    {
        var month = clock.CurrentDate().Month;

        return month switch
        {
            4 => new Percents20Policy(),
            9 => new Percents10Policy(),
            _ => new NoPercentsPolicy()
        };
    }
}