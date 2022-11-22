namespace the80by20.Services.Sale.App.Clients.Solution.DTO
{
    public class SolutionToProblemDto
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string RequiredSolutionTypes { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public string SolutionElementsGDriveLink { get; set;}

        public decimal Price { get; set; }

        public string SolutionSummary { get; set; }

        public string SolutionElements { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
