using System.Globalization;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Exceptions;

namespace the80by20.Shared.Abstractions.Kernel.Capabilities;

[ValueObjectDdd]
public sealed record Money
{
    public decimal Value { get; }

    public static Money FromValue(decimal value)
    {
        if (value < 0)
        {
            throw new DomainException(nameof(Money));
        }

        return new(value);
    }
    public static Money Zero() => new(0m);

    private Money(decimal value)
    {
        Value = value;
    }

    public bool HasValue() => Value != default;

    public static Money operator +(Money money, Money other) => new Money(money.Value + other.Value);

    public static Money operator -(Money money, Money other) => new Money(money.Value - other.Value);

    public static implicit operator decimal(Money id) => id.Value;

    public static implicit operator Money(decimal value) => new(value);

    public Money Percentage(int percentage)
    {
        if (percentage == 0)
        {
            return new Money(Value);
        }

        return new Money(Math.Round(percentage * Value / 100.0m));
    }

    public override string ToString()
    {
        return Value.ToString("0.00", CultureInfo.CreateSpecificCulture("pl-PL"));
    }

}