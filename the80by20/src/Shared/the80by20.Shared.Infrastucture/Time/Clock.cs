using the80by20.Shared.Abstractions.Time;

namespace the80by20.Shared.Infrastucture.Time;

public sealed class Clock : IClock
{
    public DateTime CurrentDate() => DateTime.UtcNow;
}