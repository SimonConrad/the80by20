using System.Text;
using FluentValidation;
using the80by20.Shared.Abstractions.Commands;
using the80by20.Shared.Abstractions.Exceptions;

namespace the80by20.Shared.Infrastucture.Decorators
{
    [Decorator]
    public sealed class ValidationCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
        where TCommand : class, ICommand
    {
        private readonly ICommandHandler<TCommand> _decorated;
        private readonly IEnumerable<IValidator<TCommand>> _validators;

        public ValidationCommandHandlerDecorator(ICommandHandler<TCommand> decorated,
            IEnumerable<IValidator<TCommand>> validators)
        {
            _decorated = decorated;
            _validators = validators;
        }

        public Task HandleAsync(TCommand command)
        {
            var errors = _validators
                .Select(v => v.Validate(command))
                .SelectMany(result => result.Errors)
                .Where(error => error is not null);

            if (errors.Any())
            {
                var errorBuilder = new StringBuilder();

                errorBuilder.AppendLine("Invalid command, reason: ");

                foreach (var error in errors)
                {
                    errorBuilder.AppendLine(error.ErrorMessage);
                }

                throw new InputValidationException(errorBuilder.ToString());
            }

            return _decorated.HandleAsync(command);
        }
    }

    public class InputValidationException : The80by20Exception
    {
        public InputValidationException(string message) : base(message)
        {
        }
    }
}