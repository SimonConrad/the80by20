﻿using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.Exceptions;

namespace the80by20.Masterdata.App.Exceptions
{
    public class CannotDeleteCategoryException : The80by20Exception
    {
        public Guid Id { get; }

        public CannotDeleteCategoryException(Guid id) : base($"Category with ID: '{id}' cannot be deleted.")
        {
            Id = id;
        }
    }
}
