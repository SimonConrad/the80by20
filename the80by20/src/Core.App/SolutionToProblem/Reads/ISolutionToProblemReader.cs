namespace Core.App.SolutionToProblem.Reads;

public interface ISolutionToProblemReader
{
    Task<SolutionToProblemReadModel> Get(Guid solutionToProblemId);
}