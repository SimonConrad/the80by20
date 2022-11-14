namespace the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

public class CommandCqrsAttribute : Attribute
{ }

public class CommandHandlerCqrsAttribute : Attribute
{ }

public class QueryHandlerCqrsAttribute : Attribute
{ }

// INFO Query do not modify application state do not have any side effects
public class QueryCqrsAttribute : Attribute
{ }