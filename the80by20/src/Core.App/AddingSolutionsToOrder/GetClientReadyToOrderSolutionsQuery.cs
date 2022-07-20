namespace Core.App.AddingSolutionsToOrder
{
    public class GetClientReadyToOrderSolutionsQuery
    {
        // todo vo order / dto
        IEnumerable<Guid> Get(Guid clientId)
        {
            return new List<Guid> { Guid.NewGuid() };
        }
    }
}