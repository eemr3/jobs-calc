using JobsCalc.Communication.DTOs.Requests;
using JobsCalc.Communication.DTOs.Responses;

namespace JobsCalc.Domain.Interfaces.UseCases;

public interface IAuthUseCase
{
  public Task<LoginResponse> Execute(LoginRequest request);
}