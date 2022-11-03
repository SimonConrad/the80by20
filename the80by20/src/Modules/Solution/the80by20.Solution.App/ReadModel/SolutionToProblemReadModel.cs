using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

namespace the80by20.Modules.Solution.App.ReadModel;

/// <summary>
/// info denormalized (in db) model, optimized for fast queries without unecessary joins, on event storming model a decision data for doing or not command
/// updated in db always as a a reaction that event happened, so every data, updated in commands catalog,
/// if we have crud logic as in admisnitaration where will be jus generic crud repo there 
/// in case of event sourcing also done this way
/// info denormalized (optimized for fast reads, and scope od data read from different sources) view consisting of projection of:
/// aggregate invariant attributes, related to aggregate crud data, and others
/// dedicated for command deciding to do, based on es model
/// </summary>

// info przy osobny readmodelu problem z obsluga mechanizmu optimistic concurrency, moze rozwiazaniem jest pdczas
// pobierania readmodelu pobierac rowniez id-wersji z tabeli agregatu i przy zapisie poronywac?
// albo zrobic zapis agregata i czytanie z niego danych z tej samej tabeli, ale na czytanie obiekt poco - readmodel zwrocic jsnoem

// TODO different readmodel to client and to admin
[ReadModelDdd]
public class SolutionToProblemReadModel
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string RequiredSolutionTypes { get; set; }

    public string Description { get; set; }

    public string Category { get; set; }

    public Guid? CategoryId { get; set; }

    public bool IsConfirmed { get; set; }

    public bool IsRejected { get; set; }

    public Guid? SolutionToProblemId { get; set; }

    public decimal? Price { get; set; }

    public string SolutionSummary { get; set; }

    public string SolutionElements { get; set; }

    public bool WorkingOnSolutionEnded { get; set; }

    public DateTime CreatedAt { get; set; }
}