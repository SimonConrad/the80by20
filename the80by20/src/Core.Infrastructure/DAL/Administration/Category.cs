using Common.DDD;

namespace Core.Infrastructure.DAL.Administration;

[CrudEntity]
public class Category
{
    public int Id { get; private set; }
    public string Name { get; set; }
}