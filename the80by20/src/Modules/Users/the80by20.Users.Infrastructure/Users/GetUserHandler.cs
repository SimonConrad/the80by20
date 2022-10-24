using Microsoft.EntityFrameworkCore;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Queries;
using the80by20.Users.App.Queries;
using the80by20.Users.Domain.UserEntity;
using the80by20.Users.Infrastructure.EF;

namespace the80by20.Users.Infrastructure.Users;

[QueryHandlerCqrs]
public sealed class GetUserHandler : IQueryHandler<GetUser, UserDto>
{
    private readonly UsersDbContext _dbContext;

    public GetUserHandler(UsersDbContext dbContext)
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

