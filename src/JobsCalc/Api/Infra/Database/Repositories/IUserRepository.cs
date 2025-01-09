using JobsCalc.Api.Domain.Entities;

namespace JobsCalc.Api.Infra.Database.Repositories;
public interface IUserRepository
{
  public Task<User> AddUser(User user);
  public Task<User?> GetUserById(int userId);
  public Task<User?> GetUserByEmail(string email);
  public Task<User> UpdateUser(User user);
  public Task DeleteUser(int userId);
}