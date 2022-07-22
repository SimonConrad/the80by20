using System.Runtime.CompilerServices;
using Core.App.SolutionToProblem;
using Core.Domain.SharedKernel;

namespace WebApi.Core;

public class CreateProblemDto
{
    public string Description { get; set; }
        
    public string Category { get; set; }

    public Guid UserId { get; set; }

    public SolutionElementType[] SolutionElementTypes { get; set; }

    public  CreateProblemCommand MapToCommand()
    {
        return new CreateProblemCommand()
        {
            Description = this.Description,
            SolutionElementTypes = this.SolutionElementTypes,
            Category = this.Category,
            UserId = this.UserId
        };
    }
}