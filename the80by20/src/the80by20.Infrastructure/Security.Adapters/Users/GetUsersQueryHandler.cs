using the80by20.App.Security.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using the80by20.Infrastructure.DAL.DbContext;

namespace the80by20.Infrastructure.Security.Adapters.Users;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserDto>>
{
    private readonly CoreDbContext _dbContext;

    public GetUsersQueryHandler(CoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<UserDto>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
        => await _dbContext.Users
            .AsNoTracking()
            .Select(x => x.AsDto())
            .ToListAsync(cancellationToken);
}