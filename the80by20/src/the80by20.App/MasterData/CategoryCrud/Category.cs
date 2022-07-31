using the80by20.Common.ArchitectureBuildingBlocks.MarkerAttributes;

namespace the80by20.App.MasterData.CategoryCrud;
// todo show concpet of soft delete in this crud module, inrerceptor setting isdeleted whene remove from dbcotxt, interceptor don returns is-delted
// todo show audit mechanism in thi crud-module
[CrudEntity]
public class Category
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }

    public static Category WithCustomId(Guid id, string name) => new Category() { Id = id, Name = name };
    public static Category WithGeneratedId(string name) => new Category() { Id = Guid.NewGuid(), Name = name };
}