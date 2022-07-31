﻿using the80by20.Common.ArchitectureBuildingBlocks;
using the80by20.Common.ArchitectureBuildingBlocks.Exceptions;

namespace the80by20.Domain.Security.UserEntity.Exceptions;

public sealed class InvalidEmailException : CustomException
{
    public string Email { get; }

    public InvalidEmailException(string email) : base($"Email: '{email}' is invalid.")
    {
        Email = email;
    }
}