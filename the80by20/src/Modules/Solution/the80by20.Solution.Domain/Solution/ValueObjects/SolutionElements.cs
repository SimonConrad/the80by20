using System.Collections.Immutable;
using System.Text.Json;
using the80by20.Modules.Solution.Domain.Shared;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Exceptions;

namespace the80by20.Modules.Solution.Domain.Solution.ValueObjects;

[ValueObjectDdd]
public sealed class SolutionElements
{
    // immutability achieved by init, and collection and SolutionElement as record
    public ImmutableHashSet<SolutionElement> Elements { get; init; }

    private SolutionElements(ImmutableHashSet<SolutionElement> elements)
    {
        Elements = elements;
    }

    public static SolutionElements Empty() => new(ImmutableHashSet.Create<SolutionElement>());

    public static SolutionElements FromSnapshotInJson(string snapshot)
    {
        var deserialSnapshot = JsonSerializer.Deserialize<SolutionElementSnapshot[]>(snapshot);

        var temp = deserialSnapshot.Select(x => SolutionElement.From(x.Type, x.Link)).ToImmutableHashSet();

        return new(temp);
    }

    public SolutionElements Add(SolutionElement element)
    {
        // info will throw when adding same SolutionElement valuobject as _elements is hashset and SolutionElement is record
        // todo test
        try
        {
            var elements = Elements.Add(element);
            return new(elements);
        }
        catch (Exception e)
        {
            throw new DomainException(nameof(SolutionElements));
        }
    }

    public SolutionElements Remove(SolutionElement element)
    {
        return new(Elements.Remove(element)); // immutability
    }

    public bool HaveAllRequiredElementTypes(RequiredSolutionTypes req) =>
        req.Elements.All(r => Elements.Select(x => x.Type).Contains(r));


    public string ToSnapshotInJson()
    {
        var snapshot = Elements.Select(x => new SolutionElementSnapshot() { Type = x.Type, Link = x.Link });

        return JsonSerializer.Serialize(snapshot);
    }

    // todo think if this class can be private
    public class SolutionElementSnapshot
    {
        public SolutionType Type { get; set; }
        public string Link { get; set; }
    }

}