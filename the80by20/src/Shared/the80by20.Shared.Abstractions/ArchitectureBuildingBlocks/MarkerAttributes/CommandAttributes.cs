namespace the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

/// <summary>
/// command part of cqrs pattern, in contrast to query command change state of the system (have side-effects)
///
/// id problem:
/// - nice if command-handler do not return data, if we want id in creation-command we can create it before sending command
/// natural key-are nice
/// - but we can be also pragmatic and return id from command handler, when id is generated in database
/// - hibride - database has own id + snowflake-id
/// 
/// cqs - separation on method level vs cqrs on objects level (command + command-handler, query + query-handler)
/// 
/// full blown with separate write-side and query-side (separate stores)
/// only on full-blown level of cqrs one can apply event-sourcing
/// </summary>
public class CommandCqrsAttribute : Attribute
{ }

public class CommandHandlerCqrsAttribute : Attribute
{ }

public class QueryHandlerCqrsAttribute : Attribute
{ }

