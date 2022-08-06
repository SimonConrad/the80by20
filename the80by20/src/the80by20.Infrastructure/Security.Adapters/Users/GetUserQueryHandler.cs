using Microsoft.EntityFrameworkCore;
using the80by20.App.Abstractions;
using the80by20.App.Security.Queries;
using the80by20.Domain.Security.UserEntity;
using the80by20.Infrastructure.DAL.DbContext;

namespace the80by20.Infrastructure.Security.Adapters.Users;

public sealed class GetUserQueryHandler : IQueryHandler<GetUser, UserDto>
{
    private readonly CoreDbContext _dbContext;

    public GetUserQueryHandler(CoreDbContext dbContext) 
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

