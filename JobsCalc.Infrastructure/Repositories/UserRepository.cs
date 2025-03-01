using JobsCalc.Domain.Entities;
using JobsCalc.Domain.Interfaces.Repositories;
using JobsCalc.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace JobsCalc.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDbContextFactory<ApiDbContext> _contextFactory;

    public UserRepository(IDbContextFactory<ApiDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<UserEntity> RegisterUserAsync(UserEntity userEntity)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var newUser = await context.Users.AddAsync(userEntity);

        await context.SaveChangesAsync();

        return newUser.Entity;
    }

    public async Task<UserEntity?> GetUserByIdAsync(int userId)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var user = await context.Users.FirstOrDefaultAsync(user => user.UserId.Equals(userId));

        return user;
    }

    public async Task<UserEntity?> GetUserByEmailAsync(string email)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var user = await context.Users.FirstOrDefaultAsync(user => user.Email.Equals(email));

        return user;
    }

    public async Task<UserEntity?> UpdateUserAsync(UserEntity userEntity)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        context.Users.Update(userEntity);
        await context.SaveChangesAsync();
    
        return userEntity;
    }
}