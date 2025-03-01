using JobsCalc.Application.Exceptions;
using JobsCalc.Communication.DTOs.Requests;
using JobsCalc.Communication.DTOs.Responses;
using JobsCalc.Domain.Entities;
using JobsCalc.Domain.Interfaces.Repositories;
using JobsCalc.Domain.Interfaces.Services;
using JobsCalc.Domain.Interfaces.UseCases;

namespace JobsCalc.Application.UseCases.User;

public class UserUpdateUseCase : IUserUpdateUseCase
{
  private readonly IUserRepository _userRepository;
  private readonly IPasswordHasher _passwordHasher;

  public UserUpdateUseCase(IUserRepository userRepository, IPasswordHasher passwordHasher)
  {
    _userRepository = userRepository;
    _passwordHasher = passwordHasher;
  }

  public async Task<UserResponse> Execute(int userId, UserPathRequest request)
  {
    var user = await _userRepository.GetUserByIdAsync(userId)
        ?? throw new NotFoundException($"O usuário com ID {userId} não foi encontrado");
    
    await ValidateAndUpdateEmailAsync(user, request.Email);
    
    UpdateUserFields(user, request);
    
    await _userRepository.UpdateUserAsync(user);
    
    return new UserResponse
    {
      UserId = userId,
      FullName = user.FullName,
      Email = user.Email,
      AvatarUrl = user.AvatarUrl,
    };
  }

  private async Task ValidateAndUpdateEmailAsync(UserEntity user, string? email)
  {
    if (string.IsNullOrEmpty(email) || user.Email == email) return;

    if (await _userRepository.GetUserByEmailAsync(email) is not null)
      throw new ConflictException("E-mail já em uso por outro usuário.");
    
    user.Email = email;
  }

  private void UpdateUserFields(UserEntity user, UserPathRequest request)
  {
    if(!string.IsNullOrEmpty(request.FullName)&& user.FullName != request.FullName)
      user.FullName = request.FullName;
    
    if(!string.IsNullOrEmpty(request.Email) && user.Email != request.Email)
      user.Email = request.Email;
    
    if(!string.IsNullOrEmpty(request.Password))
      user.PasswordHash = _passwordHasher.HashPassword(request.Password);
    
    if(!string.IsNullOrEmpty(request.AvatarUrl))
      user.AvatarUrl = request.AvatarUrl;
  }
}
