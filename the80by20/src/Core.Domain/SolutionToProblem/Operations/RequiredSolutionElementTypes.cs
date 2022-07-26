﻿using System.Collections.Immutable;
using System.Text.Json;
using Common.DDD;
using Core.Domain.SharedKernel.Capabilities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Core.Domain.SolutionToProblem.Operations;

[ValueObjectDdd]
public class RequiredSolutionElementTypes : IEquatable<RequiredSolutionElementTypes>
{
    public IEnumerable<SolutionElementType> Elements => _elements;
    private readonly ImmutableHashSet<SolutionElementType> _elements;

    private RequiredSolutionElementTypes(ImmutableHashSet<SolutionElementType> ihs)
    {
        _elements = ihs;
    }

    public static RequiredSolutionElementTypes From(params SolutionElementType[] elements)
    {
        var ihs = elements.Distinct().ToImmutableHashSet();
        return new(ihs);
    }

    public static RequiredSolutionElementTypes FromSnapshotInJson(string snapshotInJson)
    {
        var elements = JsonSerializer.Deserialize<SolutionElementType[]>(snapshotInJson);

        if (elements == null)
        {
            throw new DomainException(nameof(RequiredSolutionElementTypes));
        }

        return RequiredSolutionElementTypes.From(elements);
    }

    public string ToSnapshotInJson()
    {
        var snapshotInJson = JsonSerializer.Serialize(Elements);
        return snapshotInJson;
    }

    public bool Equals(RequiredSolutionElementTypes? other)
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

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((RequiredSolutionElementTypes)obj);
    }

    public override int GetHashCode()
    {
        return _elements.GetHashCode();
    }
}