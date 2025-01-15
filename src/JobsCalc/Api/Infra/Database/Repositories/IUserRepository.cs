using JobsCalc.Api.Domain.Entities;
using JobsCalc.Api.Http.Dtos;

namespace JobsCalc.Api.Infra.Database.Repositories;
public interface IUserRepository
{
  public Task<User> AddUser(User user);
  public Task<User?> GetUserById(int userId);
  public Task<User?> GetUserByEmail(string email);
  public Task<User?> UpdateUser(int userId, UserPatchDto user);
  public Task DeleteUser(int userId);
}