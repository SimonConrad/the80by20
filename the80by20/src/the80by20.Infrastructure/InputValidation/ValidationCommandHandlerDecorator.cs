﻿using System.Text;
using FluentValidation;
using the80by20.App.Abstractions;

namespace the80by20.Infrastructure.InputValidation;

internal sealed class ValidationCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
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