namespace the80by20.Shared.Abstractions.Dal;

public interface IUnitOfWork
{
    Task ExecuteAsync(Func<Task> action);
}