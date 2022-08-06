﻿using the80by20.Common.ArchitectureBuildingBlocks.Exceptions;

namespace the80by20.Domain.Security.UserEntity.Exceptions;

public sealed class InvalidFullNameException : CustomException
{
    public string FullName { get; }

    public InvalidFullNameException(string fullName) : base($"Full name: '{fullName}' is invalid.")
    {
        FullName = fullName;
    }
}