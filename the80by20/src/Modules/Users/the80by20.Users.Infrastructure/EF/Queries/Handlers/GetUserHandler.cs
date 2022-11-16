using Microsoft.EntityFrameworkCore;
using the80by20.Modules.Users.App.DTO;
using the80by20.Modules.Users.App.Queries;
using the80by20.Modules.Users.Domain.UserEntity;
using the80by20.Modules.Users.Infrastructure.EF.Mappings;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Queries;

namespace the80by20.Modules.Users.Infrastructure.EF.Queries.Handlers;


// TODO
// apply same structure of infrastructure ef directories in module solution

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
            .AsNoTracking() // INFO AsNoTracking
            .SingleOrDefaultAsync(x => x.Id == userId);

        return user?.AsDto();
    }
}