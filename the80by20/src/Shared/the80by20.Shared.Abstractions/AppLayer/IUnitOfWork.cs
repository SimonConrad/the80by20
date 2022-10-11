namespace the80by20.Shared.Abstractions.AppLayer;

public interface IUnitOfWork
{
    Task ExecuteAsync(Func<Task> action);
}