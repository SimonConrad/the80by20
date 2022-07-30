﻿using the80by20.App.Administration.Security.User.Exceptions;
using the80by20.Domain.ArchitectureBuildingBlocks;

namespace the80by20.App.Administration.Security.User;

[ValueObjectDdd]
public sealed record FullName
{
    public string Value { get; }
        
    public FullName(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length is > 100 or < 3)
        {
            throw new InvalidFullNameException(value);
        }
            
        Value = value;
    }

    public static implicit operator FullName(string value) => value is null ? null : new FullName(value);

    public static implicit operator string(FullName value) => value?.Value;

    public override string ToString() => Value;
}