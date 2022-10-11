namespace the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

/// <summary>
/// TODO use
/// INFO Base Aggregate with collection of domain events, can be used for event sourcing, messaging,
/// for testing results of invoking command on aggregate (alternative verify getters)</summary>
public class DomainEventDddAttribute : Attribute
{ }