using JobsCalc.Application.Exceptions;
using JobsCalc.Communication.DTOs.Requests;
using JobsCalc.Communication.DTOs.Responses;
using JobsCalc.Domain.Interfaces;

namespace JobsCalc.Application.UseCases.Auth;

public class AuthUseCase : IAuthUseCase
{
  private readonly IUserRepository _userRepository;
  private readonly IJwtTokenGenerator _jwtTokenGenerator;
  private readonly IPasswordHasher _passwordHasher;

  public AuthUseCase(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator, IPasswordHasher passwordHasher)
  {
    _userRepository = userRepository;
    _jwtTokenGenerator = jwtTokenGenerator;
    _passwordHasher = passwordHasher;
  }

  public async Task<LoginResponse> SignIn(LoginRequest request)
  {
    var user = await _userRepository.GetUserByEmailAsync(request.Email);
    var isVerifyPassword = _passwordHasher.VerifyHashedPassword(user.PasswordHash, request.Password);
    
    if (user is null || isVerifyPassword == false) throw new InvalidLoginException();

    return new LoginResponse
    {
      access_token = _jwtTokenGenerator.GenerateToken(user.UserId)
    };
  }
}