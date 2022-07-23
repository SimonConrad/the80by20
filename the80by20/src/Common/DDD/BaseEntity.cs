namespace Common.DDD;

public class BaseEntity
{
    public Guid Id { get; protected set; } // INFO protected to achieve encapsulation
    private int? Version { get; set; } // TODO handle concurrency problem by optimistic concurrency with version
}

public interface IVersionable
{
    public int? Version { get; set; }
}