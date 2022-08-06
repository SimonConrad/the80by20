using the80by20.App.Security.Queries;
using Microsoft.EntityFrameworkCore;
using the80by20.App.Abstractions;
using the80by20.Infrastructure.DAL.DbContext;

namespace the80by20.Infrastructure.Security.Adapters.Users;

public sealed class GetUsersQueryHandler : IQueryHandler<GetUsers, IEnumerable<UserDto>>
{
    private readonly CoreDbContext _dbContext;

    public GetUsersQueryHandler(CoreDbContext dbContext) 
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