using FluentValidation;
using the80by20.Modules.Solution.App.Problem.Commands;

namespace the80by20.Modules.Solution.App.Commands.Problem.Handlers;

public sealed class CreateProblemInputValidator : AbstractValidator<RequestProblemCommand>
{
    public CreateProblemInputValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required.")
            .MinimumLength(8)
            .WithMessage("Min length is 8");
    }
}