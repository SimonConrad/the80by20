﻿using Microsoft.EntityFrameworkCore;
using the80by20.Shared.Abstractions.AppLayer;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Solution.App.Security.Queries;
using the80by20.Solution.Domain.Security.UserEntity;
using the80by20.Solution.Infrastructure.EF;

namespace the80by20.Solution.Infrastructure.Security.Adapters.Users;

[QueryHandlerCqrs]
public sealed class GetUserHandler : IQueryHandler<GetUser, UserDto>
{
    private readonly SolutionDbContext _dbContext;

    public GetUserHandler(SolutionDbContext dbContext)
        => _dbContext = dbContext;

    public async Task<UserDto> HandleAsync(GetUser query)
    {
        var userId = new UserId(query.UserId);
        var user = await _dbContext.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == userId);

        return user?.AsDto();
    }
}

//public class GetUserQueryHandlerMediatR : IRequestHandler<GetUserQuery, UserDto>
//{
//    private readonly CoreDbContext _dbContext;

//    public GetUserQueryHandlerMediatR(CoreDbContext dbContext)
//    {
//        _dbContext = dbContext;
//    }

//    public async Task<UserDto> Handle(GetUserQuery query, CancellationToken cancellationToken)
//    {
//        var userId = new UserId(query.UserId);
//        var user = await _dbContext.Users
//            .AsNoTracking()
//            .SingleOrDefaultAsync(x => x.Id == userId, cancellationToken);

//        return user?.AsDto();
//    }
//}

