﻿namespace the80by20.Shared.Abstractions.SharedKernel;

public class Versionable
{
    private int? Version { get; set; } // TODO handle concurrency problem by optimistic concurrency with version
}