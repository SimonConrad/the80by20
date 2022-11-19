using FluentValidation;

namespace the80by20.Modules.Solution.App.Problem.Commands.Handlers;

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