using Humanizer;
using Microsoft.Extensions.Logging;
using the80by20.Shared.Abstractions.Commands;

namespace the80by20.Shared.Infrastucture.Decorators
{
    [Decorator]
    public sealed class LoggingCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand> where TCommand : class, ICommand
    {
        private readonly ICommandHandler<TCommand> _commandHandler;
        private readonly ILogger<LoggingCommandHandlerDecorator<TCommand>> _logger;

        public LoggingCommandHandlerDecorator(ICommandHandler<TCommand> commandHandler,
            ILogger<LoggingCommandHandlerDecorator<TCommand>> logger)
        {
            _commandHandler = commandHandler;
            _logger = logger;
        }

        public async Task HandleAsync(TCommand command)
        {
            var commandName = typeof(TCommand).Name.Underscore();
            _logger.LogInformation("Started handling a command: {CommandName}...", commandName);
            await _commandHandler.HandleAsync(command);
            _logger.LogInformation("Completed handling a command: {CommandName}.", commandName);
        }
    }
}