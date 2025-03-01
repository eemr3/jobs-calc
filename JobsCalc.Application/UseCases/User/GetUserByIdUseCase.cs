using JobsCalc.Application.Exceptions;
using JobsCalc.Communication.DTOs.Responses;
using JobsCalc.Domain.Interfaces.Repositories;
using JobsCalc.Domain.Interfaces.UseCases;

namespace JobsCalc.Application.UseCases.User;

public class GetUserByIdUseCase : IGetUserByIdUseCase
{
  private readonly IUserRepository _userRepository;

  public GetUserByIdUseCase(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public async Task<UserResponse> Execute(int userId)
  {
    var user = await _userRepository.GetUserByIdAsync(userId);

    if (user is null) throw new NotFoundException($"Usuário com id {userId} não encontrado");

    return new UserResponse
    {
      UserId = user.UserId,
      FullName = user.FullName,
      Email = user.Email,
      AvatarUrl = user.AvatarUrl,
    };
  }
}
