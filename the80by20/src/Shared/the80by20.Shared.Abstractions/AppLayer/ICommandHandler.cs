namespace the80by20.Shared.Abstractions.AppLayer;

public interface ICommandHandler<in TCommand> where TCommand : class, ICommand
{
    Task HandleAsync(TCommand command);
}