using Common.TacticalDDD;

namespace Core.Domain.SolutionToProblem;

[ValueObjectDdd]
public class SolutionElements
{
    public IReadOnlyCollection<SolutionElement> Elements => _elements;

    private HashSet<SolutionElement> _elements { get; init; } = new();


    //TODO crud and test comaprin in hashset
    public void Add(SolutionElement element)
    {
        _elements.Add(element); // todo case when added same element 
    }

    public void Remove(SolutionElement element)
    {
        _elements.Remove(element);
    }

    public bool HaveAllRequiredElementTypes(RequiredSolutionElementTypes req) => req.Elements.All(r => Elements.Select(x => x.Type).Contains(r));

}