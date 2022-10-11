namespace the80by20.Sandbox;

public class Operators
{
    [Fact]
    public void TestOperators()
    {
        decimal v1 = new decimal(10.00);
        Money m1 = v1;
        decimal v2 = m1;

        decimal v3 = new decimal(15.00);
        Money m2 = v3;

        Assert.True(m2 > m1);
    }
}

public sealed record Money
{
    public decimal Value { get; }

    public Money(decimal value)
    {
        Value = value;
    }

    public static implicit operator Money(decimal value) => new Money(value);

    public static implicit operator decimal(Money money) => money.Value;

    public static bool operator <(Money m1, Money m2) => m1.Value < m2.Value;
    public static bool operator >(Money m1, Money m2) => m1.Value > m2.Value;
}