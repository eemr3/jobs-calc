using JobsCalc.Api.Http.Dtos;

namespace JobsCalc.Api.Application.Services.AuthService;

public interface IAuthService
{
  Task<string> SignIn(LoginDtoRequest login);
}