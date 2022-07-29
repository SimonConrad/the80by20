namespace Common.DDD;

public class Versionable
{
    private int? Version { get; set; } // TODO handle concurrency problem by optimistic concurrency with version
}