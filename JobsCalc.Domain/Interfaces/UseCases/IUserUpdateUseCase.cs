using JobsCalc.Communication.DTOs.Requests;
using JobsCalc.Communication.DTOs.Responses;

namespace JobsCalc.Domain.Interfaces.UseCases;

public interface IUserUpdateUseCase
{
  public Task<UserResponse> Execute(int userId, UserPathRequest request);
}