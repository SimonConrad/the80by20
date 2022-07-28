using Common.DDD;

namespace Core.App.Administration;

[CrudEntity]
public class Category
{
    public int Id { get; private set; }
    public string Name { get; set; }
}