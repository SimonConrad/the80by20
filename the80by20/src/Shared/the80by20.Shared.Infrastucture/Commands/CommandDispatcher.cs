using Microsoft.Extensions.DependencyInjection;
using the80by20.Shared.Abstractions.Commands;

namespace the80by20.Shared.Infrastucture.Commands
{
    internal sealed class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;

        public async Task SendAsync<TCommand>(TCommand command) where TCommand : class, ICommand
        {
            if (command is null)
            {
                return;
            }

            using var scope = _serviceProvider.CreateScope();
            var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();
            await handler.HandleAsync(command);
        }
    }
}
