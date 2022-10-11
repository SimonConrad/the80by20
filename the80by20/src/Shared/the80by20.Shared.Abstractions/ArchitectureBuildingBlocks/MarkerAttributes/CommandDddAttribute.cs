namespace the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

public class CommandDddAttribute : Attribute
{
}

public class CommandCqrsAttribute : Attribute
{ }

public class CommandHandlerCqrsAttribute : Attribute
{ }

public class QueryHandlerCqrsAttribute : Attribute
{ }

public class QueryCqrsAttribute : Attribute
{ }