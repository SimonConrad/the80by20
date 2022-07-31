﻿using Microsoft.EntityFrameworkCore;
using the80by20.App.Security.Ports;
using the80by20.Domain.Security.UserEntity;
using the80by20.Infrastructure.DAL.DbContext;

namespace the80by20.Infrastructure.Security.Adapters.Users;

public sealed class UserRepository : IUserRepository
{
    private readonly CoreDbContext _dbContext;

    public UserRepository(CoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<User> GetByIdAsync(UserId id) 
        => _dbContext.Users.SingleOrDefaultAsync(x => x.Id == id);

    public Task<User> GetByEmailAsync(Email email) 
        => _dbContext.Users.SingleOrDefaultAsync(x => x.Email == email);

    public Task<User> GetByUsernameAsync(Username username) 
        => _dbContext.Users.SingleOrDefaultAsync(x => x.Username == username);

    public async Task AddAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }
}