using the80by20.Domain.SharedKernel;

namespace the80by20.Infrastructure.Time;

public sealed class Clock : IClock
{
    public DateTime Current() => DateTime.UtcNow;
}