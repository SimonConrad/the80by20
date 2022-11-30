namespace the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

// INFO
// Query do not modify application state do not have any side effects
/// query part of cqrs pattern, in contrast to command query does not change state of the system (do not have side-effects)
/// query is idempotent 
public class QueryCqrsAttribute : Attribute
{ }