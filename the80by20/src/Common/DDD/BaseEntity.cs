namespace Common.DDD;

public class BaseEntity
{
    public Guid Id { get; protected set; } // INFO protected to achieve encapsulation
    private int? Version { get; set; }
}