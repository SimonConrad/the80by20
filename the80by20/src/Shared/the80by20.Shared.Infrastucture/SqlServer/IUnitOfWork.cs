namespace the80by20.Shared.Infrastucture.SqlServer
{
    public interface IUnitOfWork
    {
        Task ExecuteAsync(Func<Task> action);
    }
}
