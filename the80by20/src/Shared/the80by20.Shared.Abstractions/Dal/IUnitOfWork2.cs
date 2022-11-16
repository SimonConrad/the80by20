namespace the80by20.Shared.Abstractions.Dal;

public interface IUnitOfWork2
{
    Task ExecuteAsync(Func<Task> action);
}