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

  public Task<User?> GetUserById(int userId)
  {
    throw new NotImplementedException();
  }
  public async Task<User?> GetUserByEmail(string email)
  {
    var user = await _context.Users.FirstOrDefaultAsync(usr => usr.Email.Equals(email));
    if (user is null) return null;

    return user;
  }

  public Task<User> UpdateUser(User user)
  {
    throw new NotImplementedException();
  }

  public Task DeleteUser(int userId)
  {
    throw new NotImplementedException();
  }
}