using the80by20.Shared.Abstractions.DomainLayer.SharedKernel;

namespace the80by20.Shared.Infrastucture.Time;

public sealed class Clock : IClock
{
    public DateTime Current() => DateTime.UtcNow;
}