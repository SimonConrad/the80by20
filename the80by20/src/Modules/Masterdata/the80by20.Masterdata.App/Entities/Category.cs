using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

// INFO Due to the fact that this entity, and this module (masterdata) has no deep logic and it is basically simple crud
// then it maybe good idea (from pragmatic and kiss reason) to just create there anemic entity without encapsulation (no private setters) and without factory methods
namespace the80by20.Modules.Masterdata.App.Entities;
[CrudEntity]
public class Category
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public static Category WithCustomId(Guid id, string name, string description = null)
        => new Category() { Id = id, Name = name, Description = description };
    public static Category WithGeneratedId(string name, string description = null)
        => new Category() { Id = Guid.NewGuid(), Name = name, Description = description };

    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
    }
}