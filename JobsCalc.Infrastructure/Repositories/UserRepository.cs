using JobsCalc.Communication.DTOs;
using JobsCalc.Domain.Entities;
using JobsCalc.Domain.Interfaces;
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
        using var _context = _contextFactory.CreateDbContext();
        var newUser = await _context.Users.AddAsync(userEntity);

        await _context.SaveChangesAsync();

        return newUser.Entity;
    }

    public async Task<UserEntity?> GetUserByIdAsync(int userId)
    {
        using var _context = _contextFactory.CreateDbContext();
        var user = await _context.Users.FirstOrDefaultAsync(user => user.UserId.Equals(userId));

        return user;
    }

    public async Task<UserEntity?> GetUserByEmailAsync(string email)
    {
        using var _context = _contextFactory.CreateDbContext();
        var user = await _context.Users.FirstOrDefaultAsync(user => user.Email.Equals(email));

        return user;
    }

    public async Task<UserEntity?> UpdateUserAsync(UserEntity userEntity)
    {
        using var _context = _contextFactory.CreateDbContext();
        _context.Users.Update(userEntity);
        await _context.SaveChangesAsync();

        return userEntity;
    }
}