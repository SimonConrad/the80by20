﻿using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.Exceptions;

namespace the80by20.Masterdata.App.Exceptions
{
    public class ConferenceNotFoundException : The80by20Exception
    {
        public Guid Id { get; }

        public ConferenceNotFoundException(Guid id) : base($"conference with ID: '{id}' was not found.")
        {
            Id = id;
        }
    }
}