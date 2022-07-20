namespace Core.App.AddingSolutionsToOrder
{
    public class AddSolutionToOrderCommand
    {
        // todo vo guidy, encje .agergaty order, solution
        public Guid OrderId { get; set; }
        public Guid SolutionId { get; set; }
    }
}