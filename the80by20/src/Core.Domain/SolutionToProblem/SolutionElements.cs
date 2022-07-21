using Common.TacticalDDD;

namespace Core.Domain.SolutionToProblem;

[ValueObjectDdd]
public class SolutionElements
{
    // todo research alternative https://docs.microsoft.com/en-us/dotnet/api/system.collections.immutable.immutablelist-1?view=net-6.0
    public IReadOnlyCollection<SolutionElement> Elements => _elements;

    private HashSet<SolutionElement> _elements { get; init; } = new();

    
    //TODO crud and test comapsiron in hashset, implement gethaset and iequatbel of SolutionElement
    public void Add(SolutionElement element)
    {
        _elements.Add(element); // todo handle case when added same element 
    }

    public void Remove(SolutionElement element)
    {
        _elements.Remove(element);
    }

    public bool HaveAllRequiredElementTypes(RequiredSolutionElementTypes req) => req.Elements.All(r => Elements.Select(x => x.Type).Contains(r));

}