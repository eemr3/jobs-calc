using JobsCalc.Communication.DTOs;
using JobsCalc.Domain.Entities;

namespace JobsCalc.Domain.Interfaces;

public interface IUserRepository
{
    public Task<User> RegisterUserAsync(User user);
    public Task<User> GetUserByIdAsync(int userId);
    public Task<User> GetUserByEmailAsync(string email);
    public Task<User> UpdateUserAsync(UserPathRequest user);
}