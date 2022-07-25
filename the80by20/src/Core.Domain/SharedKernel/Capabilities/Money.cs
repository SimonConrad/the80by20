using System.Globalization;
using Common.DDD;

namespace Core.Domain.SharedKernel.Capabilities;

[ValueObjectDdd]
public class Money : IEquatable<Money>
{
    #region enacpasulation
    public decimal Value { get; init; }
    #endregion
    
    #region creation, rules, immuability
    public static Money FromValue(decimal value) => new(value);
    public static Money Zero() => new(0);

    private Money(decimal value)
    {
        Value = value;
    }
    #endregion

    #region behavior
    public bool HasValue() => Value != default;

    public static Money operator +(Money money, Money other)
    {
        return new Money(money.Value + other.Value);
    }

    public static Money operator -(Money money, Money other)
    {
        return new Money(money.Value - other.Value);
    }

    //public Money Percentage(int percentage)
    //{
    //    return new Money((int)Math.Round(percentage * Value / 100.0));
    //}

    //public Money Percentage(double percentage)
    //{
    //    return new Money((int)Math.Round(percentage * Value / 100));
    //}
    #endregion
   

    #region equality
    public bool Equals(Money other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj as Money == null) return false;
        return Equals((Money)obj);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public static bool operator ==(Money left, Money right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Money left, Money right)
    {
        return !Equals(left, right);
    }

    #endregion

    public override string ToString()
    {
        return Value.ToString("0.00", CultureInfo.CreateSpecificCulture("en-US"));
    }
    
}