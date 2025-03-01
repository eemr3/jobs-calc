using JobsCalc.Communication.DTOs.Responses;

namespace JobsCalc.Domain.Interfaces.UseCases;

public interface IGetUserByIdUseCase
{
  public Task<UserResponse> Execute(int userId);
}
