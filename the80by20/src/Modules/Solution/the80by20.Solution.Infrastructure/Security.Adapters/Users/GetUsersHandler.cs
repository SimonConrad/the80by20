using Microsoft.EntityFrameworkCore;
using the80by20.Shared.Abstractions.AppLayer;
using the80by20.Solution.App.Security.Queries;
using the80by20.Solution.Infrastructure.EF;

namespace the80by20.Solution.Infrastructure.Security.Adapters.Users;

public sealed class GetUsersHandler : IQueryHandler<GetUsers, IEnumerable<UserDto>>
{
    private readonly SolutionDbContext _dbContext;

    public GetUsersHandler(SolutionDbContext dbContext)
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