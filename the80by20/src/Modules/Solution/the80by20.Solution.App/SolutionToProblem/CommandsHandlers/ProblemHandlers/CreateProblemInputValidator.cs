using FluentValidation;
using the80by20.Solution.App.SolutionToProblem.Commands.ProblemCommands;

namespace the80by20.Solution.App.SolutionToProblem.CommandsHandlers.ProblemHandlers;

public sealed class CreateProblemInputValidator : AbstractValidator<CreateProblemCommand>
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