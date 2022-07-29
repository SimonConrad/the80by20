using the80by20.App.Administration.Security.User.Exceptions;
using the80by20.Domain.ArchitectureBuildingBlocks;

namespace the80by20.App.Administration.Security.User;

[ValueObjectDdd]
public sealed record Username
{
    public string Value { get; }
        
    public Username(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length is > 30 or < 3)
        {
            throw new InvalidFullNameException(value);
        }
            
        Value = value;
    }

    public static implicit operator Username(string value) => new Username(value);

    public static implicit operator string(Username value) => value?.Value;

    public override string ToString() => Value;
}