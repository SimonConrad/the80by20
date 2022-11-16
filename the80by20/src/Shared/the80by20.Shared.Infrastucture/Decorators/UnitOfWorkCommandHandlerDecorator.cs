using the80by20.Shared.Abstractions.Commands;
using the80by20.Shared.Abstractions.Dal;

namespace the80by20.Shared.Infrastucture.Decorators
{
    [Decorator]
    public sealed class UnitOfWorkCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand> where TCommand : class, ICommand
    {
        private readonly ICommandHandler<TCommand> _commandHandler;
        private readonly IUnitOfWork2 _unitOfWork;

        public UnitOfWorkCommandHandlerDecorator(ICommandHandler<TCommand> commandHandler, IUnitOfWork2 unitOfWork)
        {
            _commandHandler = commandHandler;
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(TCommand command)
        {
            await _unitOfWork.ExecuteAsync(() => _commandHandler.HandleAsync(command));
        }
    }
}