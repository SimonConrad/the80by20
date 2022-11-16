using Microsoft.EntityFrameworkCore;
using the80by20.Modules.Users.App.DTO;
using the80by20.Modules.Users.App.Queries;
using the80by20.Modules.Users.Infrastructure.EF.Mappings;
using the80by20.Shared.Abstractions.Queries;

namespace the80by20.Modules.Users.Infrastructure.EF.Queries.Handlers;

public sealed class GetUsersHandler : IQueryHandler<GetUsers, IEnumerable<UserDto>>
{
    private readonly UsersDbContext _dbContext;

    public GetUsersHandler(UsersDbContext dbContext)
        => _dbContext = dbContext;

    public async Task<IEnumerable<UserDto>> HandleAsync(GetUsers query)
        => await _dbContext.Users
            .AsNoTracking()
            .Select(x => x.AsDto())
            .ToListAsync();
}
