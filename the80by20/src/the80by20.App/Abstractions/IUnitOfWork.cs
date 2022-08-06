namespace the80by20.App.Abstractions;

public interface IUnitOfWork
{
    Task ExecuteAsync(Func<Task> action);
}