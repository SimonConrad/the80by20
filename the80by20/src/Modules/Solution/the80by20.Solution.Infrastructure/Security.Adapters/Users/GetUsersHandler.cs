using the80by20.App.Security.Queries;
using Microsoft.EntityFrameworkCore;
using the80by20.Infrastructure.DAL.DbContext;
using the80by20.Shared.Abstractions.AppLayer;

namespace the80by20.Infrastructure.Security.Adapters.Users;

public sealed class GetUsersHandler : IQueryHandler<GetUsers, IEnumerable<UserDto>>
{
    private readonly CoreDbContext _dbContext;

    public GetUsersHandler(CoreDbContext dbContext) 
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