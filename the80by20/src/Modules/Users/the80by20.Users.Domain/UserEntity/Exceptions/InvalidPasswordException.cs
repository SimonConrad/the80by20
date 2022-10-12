﻿using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.Exceptions;

namespace the80by20.Users.Domain.UserEntity.Exceptions;

public sealed class InvalidPasswordException : CustomException
{
    public InvalidPasswordException() : base("Invalid password.")
    {
    }
}