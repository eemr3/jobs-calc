using JobsCalc.Api.Http.Dtos;

namespace JobsCalc.Api.Application.Services.AuthService;

public interface IAuthSevice
{
  Task<string> SignIn(LoginDtoRequest login);
}