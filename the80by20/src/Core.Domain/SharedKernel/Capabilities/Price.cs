using Common.DDD;

namespace Core.Domain.SharedKernel.Capabilities;

[ValueObjectDdd]
public class Price 
{
    public decimal Value { get; init; }

    public static Price FromValue(decimal value) => new() { Value = value };

    public bool HasValue() => Value != default;
}