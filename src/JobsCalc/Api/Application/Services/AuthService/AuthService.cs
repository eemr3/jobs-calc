using JobsCalc.Api.Application.Services.JWT;
using JobsCalc.Api.Http.Dtos;
using JobsCalc.Api.Infra.Database.Repositories;

namespace JobsCalc.Api.Application.Services.AuthService;

public class AuthService : IAuthService
{
  private readonly IUserRepository _userRepository;
  private readonly TokenGenerator tokenGenerator;
  public AuthService(IUserRepository repository)
  {
    _userRepository = repository;
    tokenGenerator = new TokenGenerator();
  }
  public async Task<string> SignIn(LoginDtoRequest login)
  {
    var user = await _userRepository.GetUserByEmail(login.Email!);
    if (user is null) throw new UnauthorizedAccessException("Email address or password provided is incorrect.");

    if (!BCrypt.Net.BCrypt.Verify(login.Password, user.PasswordHash))
    {
      throw new UnauthorizedAccessException("Email address or password provided is incorrect.");
    }

    var token = tokenGenerator.Generator(user);

    return token;
  }
}