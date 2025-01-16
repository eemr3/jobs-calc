using JobsCalc.Api.Application.Exceptions;
using JobsCalc.Api.Domain.Entities;
using JobsCalc.Api.Http.Dtos;
using JobsCalc.Api.Infra.Database.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace JobsCalc.Api.Infra.Database.Repositories;

public class UserRepository : IUserRepository
{

  private readonly IAppDbContext _context;

  public UserRepository(IAppDbContext context)
  {
    _context = context;
  }

  public async Task<User> AddUser(User user)
  {

    var userCreated = _context.Users.Add(user).Entity;
    await _context.SaveChangesAsync();

    return userCreated;
  }

  public async Task<User?> GetUserById(int userId)
  {
    var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId.Equals(userId));

    if (user is null) return null;

    return user;
  }
  public async Task<User?> GetUserByEmail(string email)
  {
    var user = await _context.Users.FirstOrDefaultAsync(usr => usr.Email.Equals(email));
    if (user is null) return null;

    return user;
  }

  public async Task<User?> UpdateUser(int userId, UserPatchDto user)
  {
    var userExists = await _context.Users.FirstOrDefaultAsync(u => u.UserId.Equals(userId));
    if (userExists is null) return null;

    // Atualize apenas os campos necessários
    if (!string.IsNullOrEmpty(user.FullName) && userExists.FullName != user.FullName)
    {
      userExists.FullName = user.FullName;
    }

    if (!string.IsNullOrEmpty(user.Email) && userExists.Email != user.Email)
    {
      // Verifique se há outro usuário com o mesmo email
      var emailInUse = await _context.Users.AnyAsync(u => u.Email == user.Email && u.UserId != userId);
      if (emailInUse) throw new ConflictException("Email already in use by another user.");
      userExists.Email = user.Email;
    }

    if (!string.IsNullOrEmpty(user.AvatarUrl) && userExists.AvatarUrl != user.AvatarUrl)
    {
      userExists.AvatarUrl = user.AvatarUrl;
    }

    if (!string.IsNullOrEmpty(user.Password))
    {
      userExists.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
    }

    _context.Users.Update(userExists);
    await _context.SaveChangesAsync();

    return userExists;
  }

  public Task DeleteUser(int userId)
  {
    throw new NotImplementedException();
  }
}