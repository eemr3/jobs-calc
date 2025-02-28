using JobsCalc.Communication.DTOs.Requests;
using JobsCalc.Communication.DTOs.Responses;

namespace JobsCalc.Domain.Interfaces;

public interface IAuthUseCase
{
  public Task<LoginResponse> SignIn(LoginRequest request);
}