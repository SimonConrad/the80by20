namespace Core.Domain.SharedKernel;

public interface IClock
{
    DateTime Current();
}