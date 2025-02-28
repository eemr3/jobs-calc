using JobsCalc.Communication.DTOs;
using JobsCalc.Domain.Entities;
using JobsCalc.Domain.Interfaces;
using JobsCalc.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace JobsCalc.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApiDbContext _context;

    public UserRepository(ApiDbContext context)
    {
        _context = context;
    }
    
    public async Task<UserEntity> RegisterUserAsync(UserEntity userEntity)
    {
        var newUser = await _context.Users.AddAsync(userEntity);
        
        await _context.SaveChangesAsync();
        
        return newUser.Entity;
    }

    public async Task<UserEntity?> GetUserByIdAsync(int userId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(user => user.UserId.Equals(userId));

        return user;
    }

    public async Task<UserEntity?> GetUserByEmailAsync(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(user => user.Email.Equals(email));
        
        return user;
    }

    public async Task<UserEntity?> UpdateUserAsync(UserEntity userEntity)
    {
        _context.Users.Update(userEntity);
        await _context.SaveChangesAsync();
        
        return userEntity;
    }
}