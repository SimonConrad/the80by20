﻿using the80by20.Common.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Domain.Security.UserEntity.Exceptions;

namespace the80by20.Domain.Security.UserEntity;

[ValueObjectDdd]
public sealed record Password
{
    public string Value { get; }
        
    public Password(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length is > 200 or < 6)
        {
            throw new InvalidPasswordException();
        }
            
        Value = value;
    }

    public static implicit operator Password(string value) => new(value);

    public static implicit operator string(Password value) => value?.Value;

    public override string ToString() => Value;
}