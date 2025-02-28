using JobsCalc.Communication.DTOs;
using JobsCalc.Domain.Entities;

namespace JobsCalc.Domain.Interfaces;

public interface IUserRepository
{
    public Task<UserEntity> RegisterUserAsync(UserEntity userEntity);
    public Task<UserEntity?> GetUserByIdAsync(int userId);
    public Task<UserEntity?> GetUserByEmailAsync(string email);
    public Task<UserEntity?> UpdateUserAsync(UserEntity userEntity);
}