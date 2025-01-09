using JobsCalc.Api.Http.Dtos;

namespace JobsCalc.Api.Application.Services.AuthService;

public interface IAuthSevice
{
  Task<LoginResponse> SigIn(LoginRequest login);
}