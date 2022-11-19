namespace the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

/// <summary>
/// INFO
/// Hibryde of Saga + Process Manager
/// orchestrate process spanning over multiple modules / or one module
/// guards consistent state of application - it is business transaction
/// have mechanism of compensation - rollback appropriate commands if there was a fail
/// have state in which it is currently
/// have data
/// organise process steps
/// </summary>
public class SagaAttribute : Attribute
{ }