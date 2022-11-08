namespace the80by20.Shared.Abstractions.Kernel;

public class AggergateData
{
    public Guid AggregateId { get; set; }

    private int? Version { get; set; } // TODO handle concurrency problem by optimistic concurrency with version
}