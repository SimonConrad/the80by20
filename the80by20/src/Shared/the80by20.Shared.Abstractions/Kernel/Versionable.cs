namespace the80by20.Shared.Abstractions.Kernel;

// TODO te remove
public class Versionable
{
    private int? Version { get; set; } // TODO handle concurrency problem by optimistic concurrency with version
}
