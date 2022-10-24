using Microsoft.EntityFrameworkCore;
using the80by20.Shared.Abstractions.Queries;
using the80by20.Users.App.Queries;
using the80by20.Users.Infrastructure.EF;

namespace the80by20.Users.Infrastructure.Users;

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


//public class GetUsersQueryHandlerMediatr : IRequestHandler<GetUsersQueryMediatr, IEnumerable<UserDto>>
//{
//    private readonly CoreDbContext _dbContext;

//    public GetUsersQueryHandlerMediatr(CoreDbContext dbContext)
//    {
//        _dbContext = dbContext;
//    }

//    public async Task<IEnumerable<UserDto>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
//        => await _dbContext.Users
//            .AsNoTracking()
//            .Select(x => x.AsDto())
//            .ToListAsync(cancellationToken);
//}