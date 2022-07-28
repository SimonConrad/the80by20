using Core.Domain.SharedKernel;

namespace Core.Infrastructure.Time;

public sealed class Clock : IClock
{
    public DateTime Current() => DateTime.UtcNow;
}