namespace the80by20.App;

public interface IUnitOfWork
{
    Task ExecuteAsync(Func<Task> action);
}