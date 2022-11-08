using System.Collections.Immutable;
using System.Text.Json;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Exceptions;

namespace the80by20.Modules.Solution.Domain.Shared;

[ValueObjectDdd]
public sealed class RequiredSolutionTypes : IEquatable<RequiredSolutionTypes>
{
    public ImmutableHashSet<SolutionType> Elements { get; init; }

    private RequiredSolutionTypes(ImmutableHashSet<SolutionType> ihs)
    {
        Elements = ihs;
    }

    public static RequiredSolutionTypes From(params SolutionType[] elements)
    {
        var ihs = elements.Distinct().ToImmutableHashSet();
        return new(ihs);
    }

    public RequiredSolutionTypes Copy()
    {
        return new(Elements.ToImmutableHashSet());
    }

    public static RequiredSolutionTypes Empty() => new(ImmutableHashSet.Create<SolutionType>());

    public static RequiredSolutionTypes FromSnapshotInJson(string snapshotInJson)
    {
        var elements = JsonSerializer.Deserialize<SolutionType[]>(snapshotInJson);

        if (elements == null)
        {
            throw new DomainException(nameof(RequiredSolutionTypes));
        }

        return From(elements);
    }

    public string ToSnapshotInJson()
    {
        var snapshotInJson = JsonSerializer.Serialize(Elements);
        return snapshotInJson;
    }

    public bool Equals(RequiredSolutionTypes other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;


        //var equal = (this.Elements.All(x => other.Elements.Contains(x)))

        //INFO https://enterprisecraftsmanship.com/posts/representing-collection-as-value-object/
        var structurallyEqual = Elements
            .OrderBy(x => x)
            .SequenceEqual(other.Elements.OrderBy(x => x));

        return structurallyEqual;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((RequiredSolutionTypes)obj);
    }

    public override int GetHashCode()
    {
        return Elements.GetHashCode();
    }
}