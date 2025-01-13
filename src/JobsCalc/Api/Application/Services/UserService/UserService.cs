using BCrypt.Net;
using JobsCalc.Api.Application.Exceptions;
using JobsCalc.Api.Domain.Entities;
using JobsCalc.Api.Http.Dtos;
using JobsCalc.Api.Infra.Database.Repositories;

namespace JobsCalc.Api.Application.Services.UserService;

public class UserService : IUserService
{
  private readonly IUserRepository _userRepository;

  public UserService(IUserRepository repository)
  {
    _userRepository = repository;
  }

  public async Task<UserDtoResponse> AddUserAsync(UserDtoRequest userDto)
  {
    var userExists = await _userRepository.GetUserByEmail(userDto.Email);
    if (userExists is not null)
      throw new ConflictException($"Usuário com email {userDto.Email} já existe.");

    var passwordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

    var user = new User
    {
      FullName = userDto.FullName,
      Email = userDto.Email,
      PasswordHash = passwordHash
    };

    var userCreated = await _userRepository.AddUser(user);

    return new UserDtoResponse
    {
      UserId = userCreated.UserId,
      FullName = userCreated.FullName,
      Email = userCreated.Email,
      AvatarUrl = userCreated.AvatarUrl
    };
  }

  public async Task<UserDtoResponse> GetUserByEmail(string email)
  {
    var user = await _userRepository.GetUserByEmail(email);
    if (user is null) throw new KeyNotFoundException("User not found.");

    return new UserDtoResponse
    {
      UserId = user.UserId,
      FullName = user.FullName,
      Email = user.Email,
      AvatarUrl = user.AvatarUrl
    };
  }
  public Task DeleteUser(int id)
  {
    throw new NotImplementedException();
  }

  public Task<UserDtoResponse> GetUserById(int userId)
  {
    throw new NotImplementedException();
  }

  public Task<UserDtoResponse> UpdateUserAsync(UserDtoRequest user)
  {
    throw new NotImplementedException();
  }
}