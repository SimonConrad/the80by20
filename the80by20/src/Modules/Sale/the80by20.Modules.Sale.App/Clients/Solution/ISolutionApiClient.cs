using the80by20.Modules.Sale.App.Clients.Solution.DTO;

namespace the80by20.Modules.Sale.App.Clients.Solution
{
    public interface ISolutionApiClient
    {
        Task<SolutionToProblemDto> GetSolutionToProblemDto(Guid id);
    }
}
