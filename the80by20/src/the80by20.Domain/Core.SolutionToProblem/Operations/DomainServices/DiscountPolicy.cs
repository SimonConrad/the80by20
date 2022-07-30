using the80by20.Domain.ArchitectureBuildingBlocks;

namespace the80by20.Domain.Core.SolutionToProblem.Operations.DomainServices;

[PolicyDdd]
public interface IDiscountPolicy
{
    int PercentageDiscount();
}

[PolicyDdd]
public class Percents20Policy : IDiscountPolicy
{
    public int PercentageDiscount() => 20;
}

[PolicyDdd]
public class Percents10Policy : IDiscountPolicy
{
    public int PercentageDiscount() => 10;
}

[PolicyDdd]
public class NoPercentsPolicy : IDiscountPolicy
{
    public int PercentageDiscount() => 10;
}