using the80by20.Domain.ArchitectureBuildingBlocks;

namespace the80by20.App.Administration.MasterData;

[CrudEntity]
public class Category
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }

    public static Category WithCustomId(Guid id, string name) => new Category() { Id = id, Name = name };
    public static Category WithGeneratedId(string name) => new Category() { Id = Guid.NewGuid(), Name = name };
}