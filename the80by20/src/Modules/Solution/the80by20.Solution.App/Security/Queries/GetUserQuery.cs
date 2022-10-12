﻿using the80by20.Shared.Abstractions.AppLayer;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

namespace the80by20.Solution.App.Security.Queries;

// INFO we don't done care in querying for immutability
// as it is only for querying and not changing state, lack of encapsulation is not harm for our system like it is in command
// so it is dto not immutable record as in command

[QueryCqrs]
public class GetUser : IQuery<UserDto>
{
    public Guid UserId { get; set; }
}