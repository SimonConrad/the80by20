using System.Globalization;

namespace the80by20.Sandbox.Snippets;

// example of value object done using class, currently can be done more elegantly with record as done in the80by20.Domain
//[ValueObjectDdd]
public class Money_class : IEquatable<Money_class>
{
    #region enacpasulation
    public decimal Value { get; init; }
    #endregion
    
    #region creation, rules, immuability
    public static Money_class FromValue(decimal value) => new(value);
    public static Money_class Zero() => new(0);

    private Money_class(decimal value)
    {
        Value = value;
    }
    #endregion

    #region behavior
    public bool HasValue() => Value != default;

    public static Money_class operator +(Money_class money, Money_class other)
    {
        return new Money_class(money.Value + other.Value);
    }

    public static Money_class operator -(Money_class money, Money_class other)
    {
        return new Money_class(money.Value - other.Value);
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
    public bool Equals(Money_class other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj as Money_class == null) return false;
        return Equals((Money_class)obj);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public static bool operator ==(Money_class left, Money_class right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Money_class left, Money_class right)
    {
        return !Equals(left, right);
    }

    #endregion

    public override string ToString()
    {
        return Value.ToString("0.00", CultureInfo.CreateSpecificCulture("en-US"));
    }
    
}