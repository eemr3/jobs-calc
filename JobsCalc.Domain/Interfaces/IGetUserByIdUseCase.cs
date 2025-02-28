using JobsCalc.Communication.DTOs.Responses;

namespace JobsCalc.Domain.Interfaces;

public interface IGetUserByIdUseCase
{
  public Task<UserResponse> Execute(int userId);
}
