using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

namespace the80by20.Modules.Solution.Domain.Solution.Policies;

[PolicyDdd]
public interface IDiscountPolicy
{
    int PercentageDiscount();
}